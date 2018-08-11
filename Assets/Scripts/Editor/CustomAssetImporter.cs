// -------------------------------------------------------------------
// - A custom asset importer for Unity 3D game engine by Sarper Soher-
// - http://www.sarpersoher.com                                      -
// -------------------------------------------------------------------
// - This script utilizes the file names of the imported assets      -
// - to change the import settings automatically as described        -
// - in this script                                                  -
// -------------------------------------------------------------------


using UnityEngine;
using UnityEditor;  // Most of the utilities we are going to use are contained in the UnityEditor namespace

// We inherit from the AssetPostProcessor class which contains all the exposed variables and event triggers for asset importing pipeline
internal sealed class CustomAssetImporter : AssetPostprocessor {

    #region Methods

    //-------------Pre Processors

    // This event is raised when a texture asset is imported
    private void OnPreprocessTexture() {
        // I prefix my texture asset's file names with tex, following 3 lines say "if tex is not in the asset file name, do nothing"
        var fileNameIndex = assetPath.LastIndexOf('/');
        var fileName = assetPath.Substring(fileNameIndex + 1);

        if(!fileName.Contains(".png")) return;

        // Get the reference to the assetImporter (From the AssetPostProcessor class) and unbox it to a TextureImporter (Which is inherited and extends the AssetImporter with texture specific utilities)
        var importer = assetImporter as TextureImporter;
        
        importer.spritePixelsPerUnit = 20;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
    }

    #endregion
}