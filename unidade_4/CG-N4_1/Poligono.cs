using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Poligono : ObjetoGeometria
    {

        public Poligono(char rotulo, Objeto pai) : base(rotulo, pai)
        {
            base.PontosAdicionar(new Ponto4D(0,0,0));
            base.PontosAdicionar(new Ponto4D(10,0,0));
            base.PontosAdicionar(new Ponto4D(10,0,20));
            base.PontosAdicionar(new Ponto4D(0,0,20));
        }
        protected override void DesenharObjeto()
        {
            GL.PointSize(3);
            GL.LineWidth(5);
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex3(pto.X, pto.Y,pto.Z);
            }
            GL.End();
        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
        public override string ToString()
        {
             string retorno;
             retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
             for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
    }
}