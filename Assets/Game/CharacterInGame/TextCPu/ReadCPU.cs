using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCPU : MonoBehaviour
{
    static readonly string rootFolder = @"Assets/Game/CharacterInGame/TextCPu/Move.txt";
    public TextAsset textCPU;
    public int Count = 0;
    public int Bit = 5;
    private string s;

    private void Start()
    {
        StartWithBit();
    }
    public void StartWithBit()
    {
        PrintToFile();
    }

    void gen(int[] A, int n)
    {
        ++A[n - 1];
        for (int i = n - 1; i > 0; --i)
        {
            if (A[i] > 1)
            {
                ++A[i - 1];
                A[i] -= 2;
            }
        }
    }

    void xuat(int[] A, int n)
    {
        s += Count + ". ";
        for (int i = 0; i < n; i++)
        {
            s +=A[i];
        }
        Count++;
        s += "\n";
    }

    public void PrintToFile()
    {
        int n = 3;
       
        //Khởi tạo mảng
        int[] A = new int[n];
        //Xây dựng cấu hình đầu tiên
        for (int i = 0; i < n; i++) A[i] = 0;
        //In cấu hình hiện tại và xây dựng cấu hình kế tiếp
       
        for (int i = 0; i < Mathf.Pow(2, n); i++)
        {
            xuat(A, n);
            gen(A, n);
        }
        Debug.Log("WriteFile : " + s) ;
       
        StreamWriter writer = new StreamWriter(rootFolder, false);
        //writer.Write(s);
        //writer.Close();

    }
}
