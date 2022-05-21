﻿/**
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
    private Circulo obj_Circulo2;
    private SegReta obj_SegReta;
    private Ponto obj_Ponto;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = 0; camera.xmax = 600; camera.ymin = 0; camera.ymax = 600;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");
      
      Ponto4D p1 = new Ponto4D(300, 300);
      Ponto4D p2 = new Ponto4D(-100, -100);
      Ponto4D p3 = new Ponto4D(100, -100);
      int raio1 = 50;
      int raio2 = 200;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Ponto = new Ponto(objetoId, null, p1);
      obj_Ponto.ObjetoCor.CorR = 0; obj_Ponto.ObjetoCor.CorG = 0; obj_Ponto.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Ponto);
      objetoSelecionado = obj_Ponto;

      //P1
      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo = new Circulo(objetoId, null, p1, raio1);
      obj_Circulo.ObjetoCor.CorR = 0; obj_Circulo.ObjetoCor.CorG = 0; obj_Circulo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;

      //P2
      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo2 = new Circulo(objetoId, null, p1, raio2);
      obj_Circulo2.ObjetoCor.CorR = 0; obj_Circulo2.ObjetoCor.CorG = 0; obj_Circulo2.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Circulo2);
      objetoSelecionado = obj_Circulo2;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Retangulo = new Retangulo(objetoId, null, new Ponto4D(Matematica.GerarPtosCirculoSimétrico(raio2) + p1.X, Matematica.GerarPtosCirculoSimétrico(raio2) + p1.Y), 
                                    new Ponto4D(Matematica.GerarPtosCirculoSimétrico(raio2*-1) + p1.X, Matematica.GerarPtosCirculoSimétrico(raio2*-1) + p1.Y));
      obj_Retangulo.ObjetoCor.CorR = 255; obj_Retangulo.ObjetoCor.CorG = 255; obj_Retangulo.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_Retangulo);

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
#if CG_Gizmo
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
#endif
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
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          Console.WriteLine(objetosLista[i]);
        }
      }
#if CG_Gizmo
      else if (e.Key == Key.O)
        bBoxDesenhar = !bBoxDesenhar;
#endif
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
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
      if (obj_Circulo.BBox.obterCentro.X > obj_Circulo.BBox.obterMenorX
          && obj_Circulo.BBox.obterCentro.X < obj_Circulo.BBox.obterMaiorX) {
            if (obj_Circulo.BBox.obterCentro.X > obj_Circulo.BBox.obterMenorY
                && obj_Circulo.BBox.obterCentro.X < obj_Circulo.BBox.obterMaiorY) {
                  
            }
      } else {

      }
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
      
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
