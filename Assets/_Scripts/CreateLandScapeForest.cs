using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

//����� CreateLandScapeForest ��� ��������� ��������� ������� Forest(���)
public class CreateLandScapeForest : MonoBehaviour
{
    public Terrain terrain; //���������� terrain
    public float[,] copy_heights; //������ �����

    private int xBase = 0;//��������� ���������� �� X ������������� ����
    private int yBase = 0;//��������� ���������� �� Y ������������� ����
    private int min = 1;// ����������� �������� ����������
    private int max = 256;//������������ �������� ����������
    private int StartX;//���������� ��������� ����� �� �
    private int StartZ;//���������� ��������� ����� �� Z
    private int borderX;//����� StartX
    private int borderZ;//����� StartZ
    private int offset;//��������, �� ������� ����� ���������� ������� ��������� �����
    [Range(1,15)]
    public int size = 10;//������ �������� �����

    [Range(1,1000)]
    public int count = 1;//���-�� �������� �����

    [Range(0.0f, 0.0005f)]
    public float HeightStartValue;//��������� �������� ��������� ������

    [Range(0.00051f, 1.0f)]
    public float HeightEndValue;//�������� �������� ��������� �������

    void Start()
    {
        //�������� ���������� terrain � ������ �����
        terrain = GetComponent<Terrain>();
        copy_heights = terrain.terrainData.GetHeights(xBase, yBase, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
      
        //����������� terrain
        EditTerrainHeight();
    }

    void OnDestroy() 
    {
        //���������� ������� ����� �����
        ResetTerrainHeight();
    }

    void EditTerrainHeight()
    {
        //�������� ������ terrain
        TerrainData terrainData = terrain.terrainData;
        float[,] heights = terrainData.GetHeights(xBase, yBase, terrainData.alphamapWidth, terrainData.alphamapHeight);
        for (int i = 0; i< count; i++) {
            if(i == 0) { offset = 0; }
            else { offset = Random.Range(size, size + 15); }//set the offset from the extreme point
            StartX = Random.Range(min, max)+offset;//random start coord x to draw
            borderX = StartX;
            for (; StartX < Clamp(borderX+size); StartX++)
            {
                StartZ = Random.Range(min, max)+offset; //random start coord z to draw
                borderZ = StartZ;
                for (; StartZ < Clamp(borderZ+size); StartZ++)
                {
                    float Height = Random.Range(HeightStartValue, HeightEndValue);
                    //here you can adjust some areas so that somewhere is high and somewhere is low.
                    heights[StartZ, StartX] = Height;//HEIGHT
                   
                }
            }
         }   


        terrainData.SetHeights(xBase, yBase, heights);//SET HEIGHTS TERRAIN
    }


    void ResetTerrainHeight()
    {
        //TerrainData terrainData = terrain.terrainData;
        ////Get heights terrain
        //float[,] heights = terrainData.GetHeights(0, 0, 10, 10);



        //for (int x = 5; x < 10; x++)
        //{
        //    for (int z = 5; z < 10; z++)//
        //    {
        //        float Height = Random.Range(HeightStartValue, HeightEndValue);
        //        heights[z, x] = 0.0f;//HEIGHT

        //    }
        //}


        //terrainData.SetHeights(0, 0, heights);//SET HEIGHTS TERRAIN
        terrain.terrainData.SetHeights(xBase, yBase, copy_heights);//RESET HEIGHTS
    }

    int Clamp(int value,int min=0,int max=256)
    {
        if(value < min)
        {
            return min;
        }
        else if(value > max)
        {
            return max;
        }
        return value;
    }
}
