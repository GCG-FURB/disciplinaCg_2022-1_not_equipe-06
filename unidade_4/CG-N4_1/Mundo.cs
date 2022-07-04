/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
// #define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
     private static Mundo instanciaMundo = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraPerspective camera = new CameraPerspective();
    protected List<Objeto> objetosLista = new List<Objeto>();
    protected List<Objeto> objetosLinha = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private bool mouseMoverPto = false;
    private bool mouseAlterarPto = false;
    private Poligono obj_Poligno;
    private Cubo obj_Cubo;
    private bool novoPoligno = true;
    private float fovy, aspect, near, far;
    private Vector3 eye, at, up;
    private float speed = 1.5f;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif


    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");

      fovy = (float)Math.PI / 4;
      aspect = Width / (float)Height;
      near = 0.01f;
      far = 500.0f;
      eye = new Vector3(10, 80, 40);
      at = new Vector3(10, 0, 0);
      up = new Vector3(0, 1, 0);

      objetoId = Utilitario.charProximo(objetoId);
      obj_Poligno = new Poligono(objetoId, null);
      obj_Poligno.ObjetoCor.CorR = 255; obj_Poligno.ObjetoCor.CorG = 255; obj_Poligno.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Poligno);
      
      //Parede direita
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 25, 27, -1, 30, true);
      obj_Cubo.ObjetoCor.CorR = 255; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Cubo);
      
      //Parede esquerda
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, -1, 1, -1, 30, true);
      obj_Cubo.ObjetoCor.CorR = 255; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Cubo);

      //Parede fundo
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, -1, 26, -1, 1, true);
      obj_Cubo.ObjetoCor.CorR = 255; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Cubo);

      //Cubo controlado
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 11, 14, 27, 28, false);
      obj_Cubo.ObjetoCor.CorR = 0; obj_Cubo.ObjetoCor.CorG = 255; obj_Cubo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Cubo);
      objetoSelecionado = obj_Cubo;

      criarNovaLinha();

#if CG_Privado
      /*objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta = new Privado_SegReta(objetoId, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      obj_SegReta.ObjetoCor.CorR = 255; obj_SegReta.ObjetoCor.CorG = 255; obj_SegReta.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_SegReta);
      objetoSelecionado = obj_SegReta;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo = new Privado_Circulo(objetoId, null, new Ponto4D(100, 300), 50);
      obj_Circulo.ObjetoCor.CorR = 0; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;*/
#endif
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
    }
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, near, far);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
   
      Matrix4 modelview = Matrix4.LookAt(eye, at, up);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);
#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }

    public void tratamentoColisao(){
      for (var i = 0; i < objetosLista.Count; i++)
      {
          
      }
    }


    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.O){
        bBoxDesenhar = !bBoxDesenhar;
      } else if (e.Key == Key.W && objetoSelecionado != null){
        eye += at * speed;
      } else if (e.Key == Key.S && objetoSelecionado != null){
        eye -= at * speed;
      } else if (e.Key == Key.A && objetoSelecionado != null){
        eye -= Vector3.Normalize(Vector3.Cross(at, up)) * speed;
      } else if (e.Key == Key.D && objetoSelecionado != null){
        eye += Vector3.Normalize(Vector3.Cross(at, up)) * speed;
      } else if (e.Key == Key.Space && objetoSelecionado != null){
        eye += up * speed;
      } else if (e.Key == Key.C && objetoSelecionado != null){
        eye -= up * speed;
      } else if (e.Key == Key.Left && objetoSelecionado != null){
        if(objetoSelecionado.ObterTranslaçãoEmX() > -10){
          objetoSelecionado.TranslacaoXYZ(-2, 0, 0);
        }
      } else if (e.Key == Key.Right && objetoSelecionado != null){
        if(objetoSelecionado.ObterTranslaçãoEmX() < 10){
          objetoSelecionado.TranslacaoXYZ(2, 0, 0);
        }
      } else if (e.Key == Key.Up && objetoSelecionado != null){
        objetoSelecionado.TranslacaoXYZ(0, 0, -2);
      } else if (e.Key == Key.Down && objetoSelecionado != null){
        objetoSelecionado.TranslacaoXYZ(0, 0, 2);
      } else if (e.Key == Key.M && objetoSelecionado != null){
        objetoSelecionado.MatrizToString();
      }
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600-e.Position.Y; // Inverti eixo Y
      if (mouseMoverPto && (objetoSelecionado != null))
      {
        objetoSelecionado.PontosUltimo().X = mouseX;
        objetoSelecionado.PontosUltimo().Y = mouseY;
      }
    }

    private void criarNovaLinha(){
      //obj1
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 1, 4, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 0; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Cubo);
      //obj2
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 4, 7, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 255; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Cubo);
      //obj3
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 7, 10, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 0; obj_Cubo.ObjetoCor.CorG = 255; obj_Cubo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Cubo);
      //obj4
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 10, 13, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 0; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Cubo);
      //obj5
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 13, 16, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 255; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Cubo);
      //obj6
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 16, 19, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 0; obj_Cubo.ObjetoCor.CorG = 255; obj_Cubo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Cubo);
      //obj7
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 19, 22, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 0; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Cubo);
      //obj8
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null, 22, 25, 1, 2, false);
      obj_Cubo.ObjetoCor.CorR = 255; obj_Cubo.ObjetoCor.CorG = 0; obj_Cubo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Cubo);
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      //GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(10, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 10, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 10);
      GL.End();
    }
#endif    
  }
  
  class Program
  {
    static void Main(string[] args)
    {
      ToolkitOptions.Default.EnableHighResolution = false;

      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N2";
      window.Run(1.0 / 60.0);
    }
  }
}