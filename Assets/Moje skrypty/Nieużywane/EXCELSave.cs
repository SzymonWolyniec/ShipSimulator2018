using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

using UnityEngine.UI;


public class EXCELSave : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();


    // Use this for initialization
    void Start()
    {
        oki();
    }

    void oki()
    {

        string ruta = " pliczek.csv";

        
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }

      
        var sr = File.CreateText(ruta);

        
        string datosCSV = "valor1,valor2,valor3,valor4\n";
        datosCSV += "valor1,valor2,valor3,valor4\n";
        datosCSV += "valor1,valor2,valor3,valor4\n";
        datosCSV += "valor1,valor2,valor3,valor4\n";
        datosCSV += "valor1,valor2,valor3,valor4";

        sr.WriteLine(datosCSV);

        // Pozostaw jako tylko do odczytu
         FileInfo fInfo = new FileInfo(ruta);
         fInfo.IsReadOnly = false;

        
        sr.Close();



        // Otwórz nowo utworzony plik
        Application.OpenURL(ruta);
    }

    
}

