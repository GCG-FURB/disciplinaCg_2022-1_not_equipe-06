using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Spline : ObjetoGeometria
    {

        private Ponto4D pontoA;
        private Ponto4D pontoB;
        private Ponto4D pontoC;
        private Ponto4D pontoD;
        public long quantidade;

        public Ponto4D calcular(double t)
        {
            Ponto4D r = new Ponto4D(0,0);

                r.X = (Math.Pow(1 - t, 3) * pontoA.X) 
                    + (((3 * t) * Math.Pow(1 - t, 2)) * pontoB.X) 
                    + (((3 * Math.Pow(t, 2)) * (1 - t)) * pontoC.X) 
                    + (Math.Pow(t, 3) * pontoD.X);
                r.Y = (Math.Pow(1 - t, 3) * pontoA.Y) 
                    + (((3 * t) * Math.Pow(1 - t, 2)) * pontoB.Y) 
                    + (((3 * Math.Pow(t, 2)) * (1 - t)) * pontoC.Y) 
                    + (Math.Pow(t, 3) * pontoD.Y);
            return r;
        }
        public Spline(char rotulo, Objeto pai, Ponto4D ponto1, Ponto4D ponto2, Ponto4D ponto3, Ponto4D ponto4, long n) : base(rotulo, pai)
        {   
            pontoA = ponto1;
            pontoB = ponto2;
            pontoC = ponto3;
            pontoD = ponto4;
            quantidade = n;
        }
        protected override void DesenharObjeto()
        {
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.LineStrip);
            Ponto4D r = new Ponto4D(0,0);
            GL.Vertex2(pontoA.X, pontoA.Y);
            for (double t = 0.0; t < 1.0; t+=(1.0/quantidade)) {
                r = calcular(t);
                GL.Vertex2(r.X, r.Y);
            }
            GL.Vertex2(pontoD.X, pontoD.Y);
            GL.End();
        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
        public override string ToString()
        {
            return null;
        }
    }
}