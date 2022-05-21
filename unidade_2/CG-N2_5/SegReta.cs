using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class SegReta : ObjetoGeometria
    {

        public SegReta(char rotulo, Objeto pai, Ponto4D ptoIni, Ponto4D ptoFim) : base(rotulo, pai)
        {
            base.PontosAdicionar(ptoIni);
            base.PontosAdicionar(ptoFim);
        }

        public void atualizar(Ponto4D ptoIni, Ponto4D ptoFim) {
            base.PontosAdicionar(ptoIni);
            base.PontosAdicionar(ptoFim);
        }
        protected override void DesenharObjeto()
        {
            GL.PointSize(3);
            GL.Begin(PrimitiveType.LineLoop);
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