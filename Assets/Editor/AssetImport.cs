using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static UnityEngine.Rendering.DebugUI.Table;

public class AssetImport : MonoBehaviour
{

    [MenuItem("AssetImport/ImportSprites")]
    static void ImportSprites()
    {



        var textures = Resources.LoadAll<Texture2D>("Entities"); //AssetDatabase.LoadAllAssetsAtPath("Assets/Resources"); 

        foreach(var texture in textures)
        {
            ProcessSprite(texture); 
            GenerateSpriteLibrary(texture);
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
        int row;
        int column = 0; 
        for (int idx = 2; idx < 1536 - SliceWidth; idx += SliceWidth)
        {
            row = 0;
            for (int jdx = 5760 -2; jdx > 2302; jdx -= SliceHeight)
            {
                var spriteRect = new SpriteRect()
                {
                    rect = new Rect(idx, jdx -SliceHeight, SliceWidth, SliceHeight),
                    pivot = new Vector2(0.5f, 0.5f),
                    alignment = SpriteAlignment.Center,
                    name = $"{GetAnimationName(row)},{column}",
                    spriteID = GUID.Generate()

                };
                spriterects.Add(spriteRect);
                row++; 
            }
            column++;
        }
        SliceHeight = 192;
        SliceWidth = 192; 

        column = 0;
        for (int idx = 0; idx <= 1536 - SliceWidth; idx += SliceWidth)
        {
            row = 54; 
            for (int jdx = 2304; jdx > 0; jdx -= SliceHeight)
            {
                var spriteRect = new SpriteRect()
                {
                    rect = new Rect(idx, jdx - SliceHeight, SliceWidth, SliceHeight),
                    pivot = new Vector2(0.5f, 0.5f),
                    alignment = SpriteAlignment.Center,
                    name = $"{GetAnimationName(row)},{column}",
                    spriteID = GUID.Generate()

                };
                spriterects.Add(spriteRect);
                row++;
            }
            column++;
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

    static string GetAnimationName(int row)
    {
        switch (row)
        {
            //insert the important names here? Or leave them blank because fuck that is a lot of animations. 
            case 8:
                return "Walkup";
            case 9:
                return "Walkleft";
            case 10:
                return "Walkdown";
            case 11:
                return "Walkright"; 

            default:
                return $"{row}";
        }
    }


    static void GenerateSpriteLibrary(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        var sprites = AssetDatabase.LoadAllAssetsAtPath(path).Where(z => z is Sprite).Cast<Sprite>().ToList();
        var name = texture.name;

        var asset = ScriptableObject.CreateInstance<SpriteLibraryAsset>();
        int width = 23;

        for (int idx = 0; idx < 54; idx++)
        {
            Sprite[] toAnimate = new Sprite[width];
            int instance = idx * width; 
            for (int jdx = 0; jdx < width; jdx++)
            {
                toAnimate[jdx] = sprites.FirstOrDefault(x => x.name == $"{GetAnimationName(idx)},{jdx}"); // sprites[instance + jdx];
            }
            int count = 0; 
            foreach (var sprite in toAnimate)
            {
                asset.AddCategoryLabel(sprite, GetAnimationName(idx), $"{GetAnimationName(idx)}_{count}");
                count++;
            }
        }

        width = 8;
        for (int idx = 54; idx < 66; idx++)
        {
            Sprite[] toAnimate = new Sprite[width];
            int instance = idx * width;
            for (int jdx = 0; jdx < width; jdx++)
            {
                toAnimate[jdx] = sprites.FirstOrDefault(x => x.name == $"{GetAnimationName(idx)},{jdx}"); // sprites[instance + jdx];
            }
            int count = 0;
            foreach (var sprite in toAnimate)
            {
                asset.AddCategoryLabel(sprite, GetAnimationName(idx), $"{GetAnimationName(idx)}_{count}");
                count++;
            }
        }



        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath($"Assets/SpriteLibraries/{name}Library.asset"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(asset);

    }

}
