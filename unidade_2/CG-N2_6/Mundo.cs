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

    private CameraOrtho camera = new CameraOrtho();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private bool mouseMoverPto = false;
    private Retangulo obj_Retangulo;
    private Circulo obj_Circulo;
    private SegReta obj_SegReta;

    private Ponto4D a = new Ponto4D(-100, -100);
    private Ponto pontoA;
    private SegReta segA;
    private Ponto4D b = new Ponto4D(-100, 100);
    private Ponto pontoB;
    private SegReta segB;
    private Ponto4D c = new Ponto4D(100, 100);
    private Ponto pontoC;
    private SegReta segC;
    private Ponto4D d = new Ponto4D(100, -100);
    private Ponto pontoD;
    private Spline splineD;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = -400; camera.xmax = 400; camera.ymin = -400; camera.ymax = 400;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");

      //Ponto 1
      objetoId = Utilitario.charProximo(objetoId);
      pontoA = new Ponto(objetoId, null, this.a);
      pontoA.ObjetoCor.CorR = 0; pontoA.ObjetoCor.CorG = 0; pontoA.ObjetoCor.CorB = 0;
      objetosLista.Add(pontoA);

      //Ponto 2
      objetoId = Utilitario.charProximo(objetoId);
      pontoB = new Ponto(objetoId, null, this.b);
      pontoB.ObjetoCor.CorR = 0; pontoB.ObjetoCor.CorG = 0; pontoB.ObjetoCor.CorB = 0;
      objetosLista.Add(pontoB);

      //Ponto 3
      objetoId = Utilitario.charProximo(objetoId);
      pontoC = new Ponto(objetoId, null, this.c);
      pontoC.ObjetoCor.CorR = 0; pontoC.ObjetoCor.CorG = 0; pontoC.ObjetoCor.CorB = 0;
      objetosLista.Add(pontoC);

      //Ponto 4
      objetoId = Utilitario.charProximo(objetoId);
      pontoD = new Ponto(objetoId, null, this.d);
      pontoD.ObjetoCor.CorR = 255; pontoD.ObjetoCor.CorG = 0; pontoD.ObjetoCor.CorB = 0;
      objetosLista.Add(pontoD);
      objetoSelecionado = pontoD;

      //SegReta1p2
      objetoId = Utilitario.charProximo(objetoId);
      segA = new SegReta(objetoId, null, this.a, this.b);
      segA.ObjetoCor.CorR = 0; segA.ObjetoCor.CorG = 255; segA.ObjetoCor.CorB = 255;
      objetosLista.Add(segA);

      //SegReta2p3
      objetoId = Utilitario.charProximo(objetoId);
      segB = new SegReta(objetoId, null, this.b, this.c);
      segB.ObjetoCor.CorR = 0; segB.ObjetoCor.CorG = 255; segB.ObjetoCor.CorB = 255;
      objetosLista.Add(segB);

      //SegReta3p4
      objetoId = Utilitario.charProximo(objetoId);
      segC = new SegReta(objetoId, null, this.c, this.d);
      segC.ObjetoCor.CorR = 0; segC.ObjetoCor.CorG = 255; segC.ObjetoCor.CorB = 255;
      objetosLista.Add(segC);

      //SegReta3p4
      objetoId = Utilitario.charProximo(objetoId);
      splineD = new Spline(objetoId, null, this.a, this.b, this.c, this.d, 10);
      splineD.ObjetoCor.CorR = 255; splineD.ObjetoCor.CorG = 255; splineD.ObjetoCor.CorB = 0;
      objetosLista.Add(splineD);

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
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {

      if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.E)
      {
        if (objetoSelecionado == pontoA) {
            this.a.X -= 2;
        } else if (objetoSelecionado == pontoB) {
            this.b.X -= 2;
        } else if (objetoSelecionado == pontoC) {
            this.c.X -= 2;
        } else if (objetoSelecionado == pontoD) {
            this.d.X -= 2;
        }
        objetoId = Utilitario.charProximo(objetoId);
        splineD = new Spline(objetoId, null, this.a, this.b, this.c, this.d, 10);
        splineD.ObjetoCor.CorR = 255; splineD.ObjetoCor.CorG = 255; splineD.ObjetoCor.CorB = 0;
        objetosLista.Add(splineD);
      }
      else if (e.Key == Key.O)
        //bBoxDesenhar = !bBoxDesenhar;
        camera.ZoomOut();
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
      else if(e.Key == Key.I){
        camera.ZoomIn();
      } else if (e.Key == Key.D){
        if (objetoSelecionado == pontoA) {
            this.a.X += 2;
        } else if (objetoSelecionado == pontoB) {
            this.b.X += 2;
        } else if (objetoSelecionado == pontoC) {
            this.c.X += 2;
        } else if (objetoSelecionado == pontoD) {
            this.d.X += 2;
        }
      } else if (e.Key == Key.C){
        if (objetoSelecionado == pontoA) {
            this.a.Y += 2;
        } else if (objetoSelecionado == pontoB) {
            this.b.Y += 2;
        } else if (objetoSelecionado == pontoC) {
            this.c.Y += 2;
        } else if (objetoSelecionado == pontoD) {
            this.d.Y += 2;
        }
      } else if (e.Key == Key.B){
        if (objetoSelecionado == pontoA) {
            this.a.Y -= 2;
        } else if (objetoSelecionado == pontoB) {
            this.b.Y -= 2;
        } else if (objetoSelecionado == pontoC) {
            this.c.Y -= 2;
        } else if (objetoSelecionado == pontoD) {
            this.d.Y -= 2;
        }
      } else if (e.Key == Key.Number1) {
        objetoSelecionado.ObjetoCor.CorR = 0; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
        objetoSelecionado = pontoA;
        objetoSelecionado.ObjetoCor.CorR = 255; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
      } else if (e.Key == Key.Number2) {
        objetoSelecionado.ObjetoCor.CorR = 0; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
        objetoSelecionado = pontoB;
        objetoSelecionado.ObjetoCor.CorR = 255; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
      } else if (e.Key == Key.Number3) {
        objetoSelecionado.ObjetoCor.CorR = 0; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
        objetoSelecionado = pontoC;
        objetoSelecionado.ObjetoCor.CorR = 255; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
      } else if (e.Key == Key.Number4) {
        objetoSelecionado.ObjetoCor.CorR = 0; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
        objetoSelecionado = pontoD;
        objetoSelecionado.ObjetoCor.CorR = 255; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
      } else if (e.Key == Key.X) {
        Console.WriteLine("É XUXA");
      } else if (e.Key == Key.R) {
        this.a.X = -100;
        this.a.Y = -100;
        this.b.X = -100;
        this.b.Y = 100;
        this.c.X = 100;
        this.c.Y = 100;
        this.d.X = 100;
        this.d.Y = -100;
        objetoSelecionado.ObjetoCor.CorR = 0; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
        objetoSelecionado = pontoD;
        objetoSelecionado.ObjetoCor.CorR = 255; objetoSelecionado.ObjetoCor.CorG = 0; objetoSelecionado.ObjetoCor.CorB = 0;
      }
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
      if (mouseMoverPto && (objetoSelecionado != null))
      {
        objetoSelecionado.PontosUltimo().X = mouseX;
        objetoSelecionado.PontosUltimo().Y = mouseY;
      }
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      // GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }
#endif    
  }
  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N2";
      window.Run(1.0 / 60.0);
    }
  }
}
