using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ArchimedsLab;


[RequireComponent(typeof(MeshFilter))]
public class FloatingGameEntityRealist : GameEntity
{
  public Mesh buoyancyMesh;

  /* Te 4 tablice to tablica pamięci podręcznej, która zapobiega wykonywaniu niektórych operacji w każdej klatce. */
  tri[] _triangles;
  tri[] worldBuffer;
  tri[] wetTris;
  tri[] dryTris;

  //Te dwie zmienne przechowują liczbę poprawnych trójkątów w każdej macierzy pamięci podręcznej. Różnią się od array.Length!
  uint nbrWet, nbrDry;

  WaterSurface.GetWaterHeight realist = delegate (Vector3 pos)
  {
    const float eps = 0.1f;
    return (OceanAdvanced.GetWaterHeight(pos + new Vector3(-eps, 0F, -eps))
          + OceanAdvanced.GetWaterHeight(pos + new Vector3(eps, 0F, -eps))
          + OceanAdvanced.GetWaterHeight(pos + new Vector3(0F, 0F, eps))) / 3F;
  };

  protected override void Awake()
  {
    base.Awake();

    //Domyślnie ten skrypt wykonuje siatkę renderowania w celu obliczenia sił. Możesz go przesłonić, używając prostszej siatki.
    Mesh m = buoyancyMesh == null ? GetComponent<MeshFilter>().mesh : buoyancyMesh;
    //Konfigurowanie pamięci podręcznej dla gry. Tutaj używamy zmiennych o długim czasie gry. "game-long lifetime."
    WaterCutter.CookCache(m, ref _triangles, ref worldBuffer, ref wetTris, ref dryTris);
  }

  protected override void FixedUpdate()
  {
    base.FixedUpdate();
    if (rb.IsSleeping())
      return;
    /* Zdecydowanie zaleca się wywoływanie ich w funkcji FixedUpdate, aby zapobiec niektórym dziwnym zachowaniom */

    //Spowoduje to przygotowanie statycznej pamięci podręcznej, modyfikującej wierzchołki za pomocą obrotu i przesunięcia położenia.
    WaterCutter.CookMesh(transform.position, transform.rotation, ref _triangles, ref worldBuffer);

    /*
        Now mesh ae reprensented in World position, we can split the mesh, and split tris that are partially submerged.
        Here I use a very simple water model, already implemented in the DLL.
        You can give your own. See the example in Examples/CustomWater.

        Teraz oczekujemy powtórzeń w pozycji World, możemy podzielić siatkę i podzielić tris, które są częściowo zanurzone.
        Tutaj używam bardzo prostego modelu wody, już zaimplementowanego w bibliotece DLL.
        Możesz dać własne. Zobacz przykład w Przykłady / Zwykła woda.
    */

    WaterCutter.SplitMesh(worldBuffer, ref wetTris, ref dryTris, out nbrWet, out nbrDry, realist);
    //Ta funkcja wyliczy siły w zależności od wygenerowanych wcześniej trójkątów.
    Archimeds.ComputeAllForces(wetTris, dryTris, nbrWet, nbrDry, speed, rb);
  }

#if UNITY_EDITOR
  //Some visualizations for this buyoancy script.
  protected override void OnDrawGizmos()
  {
    base.OnDrawGizmos();

    if (!Application.isPlaying)
      return;

    Gizmos.color = Color.blue;
    for (uint i = 0; i < nbrWet; i++)
    {
      Gizmos.DrawLine(wetTris[i].a, wetTris[i].b);
      Gizmos.DrawLine(wetTris[i].b, wetTris[i].c);
      Gizmos.DrawLine(wetTris[i].a, wetTris[i].c);
    }

    Gizmos.color = Color.yellow;
    for (uint i = 0; i < nbrDry; i++)
    {
      Gizmos.DrawLine(dryTris[i].a, dryTris[i].b);
      Gizmos.DrawLine(dryTris[i].b, dryTris[i].c);
      Gizmos.DrawLine(dryTris[i].a, dryTris[i].c);
    }
  }
#endif
}
