using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class SegReta : ObjetoGeometria
    {

        public Circulo(char rotulo, Objeto pai, long raio) : base(rotulo, pai)
        {
            for (int i = 0; i < 360; i += 5) {
                base.PontosAdicionar(Matematica.GerarPtosCirculo(i, 100));  
            }
        }
        protected override void DesenharObjeto()
        {
            GL.PointSize(3);
            GL.Begin(PrimitiveType.Points);
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