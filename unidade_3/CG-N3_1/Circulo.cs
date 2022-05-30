using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoGeometria
    {
        private long raio;

        public Circulo(char rotulo, Objeto pai, Ponto4D ptoCentro, long raio) : base(rotulo, pai)
        {
            this.raio = raio;
            Ponto4D ponto = new Ponto4D();
            for (double i = ptoCentro.X; i < ptoCentro.X + 360; i += 1) {
                ponto = Matematica.GerarPtosCirculo(i, this.raio);
                ponto.X += ptoCentro.X;
                ponto.Y += ptoCentro.Y;
                base.PontosAdicionar(ponto);  
            }
        }
        public void atualizar(Ponto4D ptoCentro) {
            pontosLista.Clear();
            Ponto4D ponto = new Ponto4D();
            for (double i = ptoCentro.X; i < ptoCentro.X + 360; i += 1) {
                ponto = Matematica.GerarPtosCirculo(i, this.raio);
                ponto.X += ptoCentro.X;
                ponto.Y += ptoCentro.Y;
                base.PontosAdicionar(ponto);  
            }
        }
        protected override void DesenharObjeto()
        {
            GL.LineWidth(1);
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