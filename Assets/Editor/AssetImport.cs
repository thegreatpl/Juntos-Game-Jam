using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class AssetImport : MonoBehaviour
{

    [MenuItem("AssetImport/ImportSprites")]
    static void ImportSprites()
    {



        var textures = Resources.LoadAll<Texture2D>("Entities"); //AssetDatabase.LoadAllAssetsAtPath("Assets/Resources"); 

        foreach(var texture in textures)
        {
            ProcessSprite(texture); 
        }


    }

    static void ProcessSprite(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
        ti.isReadable = true;
        ti.textureType = TextureImporterType.Sprite;

        ti.spritePixelsPerUnit = 32;
        List<SpriteMetaData> newData = new List<SpriteMetaData>();


        var factory = new SpriteDataProviderFactories();
        factory.Init();
        var dataProvider = factory.GetSpriteEditorDataProviderFromObject(texture);
        dataProvider.InitSpriteEditorDataProvider();

        var spriterects = new List<SpriteRect>();

        //slice the sprite. 
        int SliceWidth = 64;
        int SliceHeight = 64;

        for(int idx = 2; idx < 1536 - SliceWidth; idx += SliceWidth)
        {
            for (int jdx = 5760 -2; jdx > 2302; jdx -= SliceHeight)
            {
                var spriteRect = new SpriteRect()
                {
                    rect = new Rect(idx, jdx -SliceHeight, SliceWidth, SliceHeight),
                    pivot = new Vector2(0.5f, 0.5f),
                    alignment = SpriteAlignment.Center,
                    name = $"{idx},{jdx}",
                    spriteID = GUID.Generate()

                };
                spriterects.Add(spriteRect);
            }
        }
        SliceHeight = 192;
        SliceWidth = 192; 

        for (int idx = 0; idx <= 1536 - SliceWidth; idx += SliceWidth)
        {
            for (int jdx = 2304; jdx > 0; jdx -= SliceHeight)
            {
                var spriteRect = new SpriteRect()
                {
                    rect = new Rect(idx, jdx - SliceHeight, SliceWidth, SliceHeight),
                    pivot = new Vector2(0.5f, 0.5f),
                    alignment = SpriteAlignment.Center,
                    name = $"{idx},{jdx}",
                    spriteID = GUID.Generate()

                };
                spriterects.Add(spriteRect);
            }
        }

        // for (int i = 0; i < texture.width; i += SliceWidth)
        // {
        //     for (int j = texture.height; j > 0; j -= SliceHeight)
        //     {
        //         var spriteRect = new SpriteRect()
        //         {
        //             rect = new Rect(i, j - SliceHeight, SliceWidth, SliceHeight), 
        //             pivot = new Vector2(0.5f, 0.5f), 
        //             alignment = SpriteAlignment.Center,
        //             name = (texture.height - j) / SliceHeight + ", " + i / SliceWidth, 
        //             spriteID = GUID.Generate()
        //
        //         };
        //         spriterects.Add(spriteRect);
        //         
        //     }
        // }
        dataProvider.SetSpriteRects(spriterects.ToArray()); 
        //ti.spritesheet = newData.ToArray();
        dataProvider.Apply();
        ti.SaveAndReimport();

        //AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);


    }


}
