using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Cubo : ObjetoGeometria
    {
        private float Xmin;
        private float Xmax;
        private float Zmax;
        private float Zmin;
        public bool isParede;

        public Cubo(char rotulo, Objeto pai, float xmin, float xmax, float zmin, float zmax, bool boolParede) : base(rotulo, pai)
        {
            Xmin = xmin;
            Xmax = xmax;
            Zmin = zmin;
            Zmax = zmax;
            isParede = boolParede;
            base.PontosAdicionar(new Ponto4D(xmin,-1,zmax));
            base.PontosAdicionar(new Ponto4D(xmax,-1,zmax));
            base.PontosAdicionar(new Ponto4D(xmax,1,zmax));
            base.PontosAdicionar(new Ponto4D(xmin,1,zmax));
            base.PontosAdicionar(new Ponto4D(xmin,1,zmin));
            base.PontosAdicionar(new Ponto4D(xmax,1,zmin));
            base.PontosAdicionar(new Ponto4D(xmax,-1,zmin));
            base.PontosAdicionar(new Ponto4D(xmin,-1,zmin));
             
        }
        protected override void DesenharObjeto()
        {
            GL.Begin(PrimitiveType.Quads);

            // Face da frente
            GL.Normal3(0, 0, 1);
            GL.Vertex3(Xmin, -1.0f, Zmax);
            GL.Vertex3(Xmax, -1.0f, Zmax);
            GL.Vertex3(Xmax, 1.0f, Zmax);
            GL.Vertex3(Xmin, 1.0f, Zmax);
            // Face do fundo
            GL.Normal3(0, 0, -1);
            GL.Vertex3(Xmin, -1.0f, Zmin);
            GL.Vertex3(Xmin, 1.0f, Zmin);
            GL.Vertex3(Xmax, 1.0f, Zmin);
            GL.Vertex3(Xmax, -1.0f, Zmin);
            // Face de cima
            GL.Normal3(0, 1, 0);
            GL.Vertex3(Xmin, 1.0f, Zmax);
            GL.Vertex3(Xmax, 1.0f, Zmax);
            GL.Vertex3(Xmax, 1.0f, Zmin);
            GL.Vertex3(Xmin, 1.0f, Zmin);
            // Face de baixo
            GL.Normal3(0, -1, 0);
            GL.Vertex3(Xmin, -1.0f, Zmax);
            GL.Vertex3(Xmin, -1.0f, Zmin);
            GL.Vertex3(Xmax, -1.0f, Zmin);
            GL.Vertex3(Xmax, -1.0f, Zmax);
            // Face da direita
            GL.Normal3(1, 0, 0);
            GL.Vertex3(Xmax, -1.0f, Zmax);
            GL.Vertex3(Xmax, -1.0f, Zmin);
            GL.Vertex3(Xmax, 1.0f, Zmin);
            GL.Vertex3(Xmax, 1.0f, Zmax);
            // Face da esquerda
            GL.Normal3(-1, 0, 0);
            GL.Vertex3(Xmin, -1.0f, Zmax);
            GL.Vertex3(Xmin, 1.0f, Zmax);
            GL.Vertex3(Xmin, 1.0f, Zmin);
            GL.Vertex3(Xmin, -1.0f, Zmin);

            GL.End();   
      }

        //TODO: melhorar para exibir n??o s?? a lista de pontos (geometria), mas tamb??m a topologia ... poderia ser listado estilo OBJ da Wavefrom
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