using System;
using System.Collections.Generic;

namespace CG_Biblioteca
{
  /// <summary>
  /// Classe com funções matemáticas.
  /// </summary>
  public abstract class Matematica
  {
    /// <summary>
    /// Função para calcular um ponto sobre o perímetro de um círculo informando um ângulo e raio.
    /// </summary>
    /// <param name="angulo"></param>
    /// <param name="raio"></param>
    /// <returns></returns>

    public static Ponto4D GerarPtosCirculo(double angulo, double raio)
    {
      Ponto4D pto = new Ponto4D();
      pto.X = (raio * Math.Cos(Math.PI * angulo / 180.0));
      pto.Y = (raio * Math.Sin(Math.PI * angulo / 180.0));
      pto.Z = 0;
      return (pto);
    }

    public static double GerarPtosCirculoSimétrico(double raio)
    {
      return (raio * Math.Cos(Math.PI * 45 / 180.0));
    }
    
    public static double DistanciaEuclidiana(Ponto4D pontoLista, Ponto4D pontoMouse){
      double distancia = Math.Sqrt((Math.Pow(pontoLista.X - pontoMouse.X, 2) + Math.Pow(pontoLista.Y - pontoMouse.Y, 2)));
      return distancia;
    }
  }
}