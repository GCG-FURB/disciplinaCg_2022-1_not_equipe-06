using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Spline : ObjetoGeometria
    {

        public Spline(char rotulo, Objeto pai, Ponto4D ponto1, Ponto4D ponto2, Ponto4D ponto3, Ponto4D ponto4, long n) : base(rotulo, pai)
        {   
            for (double t = 0.0; t < 1.0; t+=(1.0/n)) {
                Ponto4D r = new Ponto4D(0,0);
                double pA = Math.Pow(1 - t, 3);
                double pB = ((3 * t) * Math.Pow(1 - t, 2));
                double pC = ((3 * Math.Pow(t, 2)) * (1 - t));
                double pD = Math.Pow(t, 3);

                r.X = (Math.Pow(1 - t, 3) * ponto1.X) 
                    + (((3 * t) * Math.Pow(1 - t, 2)) * ponto2.X) 
                    + (((3 * Math.Pow(t, 2)) * (1 - t)) * ponto3.X) 
                    + (Math.Pow(t, 3) * ponto4.X);
                r.Y = (Math.Pow(1 - t, 3) * ponto1.Y) 
                    + (((3 * t) * Math.Pow(1 - t, 2)) * ponto2.Y) 
                    + (((3 * Math.Pow(t, 2)) * (1 - t)) * ponto3.Y) 
                    + (Math.Pow(t, 3) * ponto4.Y);
                base.PontosAdicionar(r);
            }
        }
        protected override void DesenharObjeto()
        {
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
            }
            GL.End();
        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
        public override string ToString()
        {
            return null;
        }
    }
}