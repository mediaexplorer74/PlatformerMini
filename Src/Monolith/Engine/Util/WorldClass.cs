
// Type: MonolithEngine.WorldLoadAndSave

// World layout enumeration

/*
Please generate and suggest an Load and Save game worlds Scene, 
giving me a working implementation.

Codeium
12:52 am, Apr 20, 2024
To implement loading and saving game worlds scenes, you can follow these steps:

Create a GameWorld class: Define a class to represent your game world, 
which may include information like player position, objects in the scene, etc.
public class GameWorld
{
    public Vector2 PlayerPosition { get; set; }
    // Add any other properties or methods related to the game world
}
Implement Load and Save methods: 
Add methods to your GameWorld class to load and save the game state.
public class GameWorld
{
    public Vector2 PlayerPosition { get; set; }
    // Add any other properties or methods related to the game world

    public void Load(string filePath)
    {
        // Implement loading game state from file
    }

    public void Save(string filePath)
    {
        // Implement saving game state to file
    }
}

Usage in your game:
To load a game world scene:
GameWorld gameWorld = new GameWorld();
gameWorld.Load("path_to_saved_file");
// Use the loaded game world data to set up your game scene

To save the current game world scene:
GameWorld gameWorld = new GameWorld();
// Update the game world state based on the current game scene
gameWorld.Save("path_to_save_file");

Implement the actual loading and saving logic:
In the Load method, read the game state data from a file and populate the GameWorld object accordingly.
In the Save method, serialize the GameWorld object to a file for later retrieval.
Remember to handle any exceptions that may occur during loading or saving, and ensure that the file paths are valid.

Feel free to customize the GameWorld class and the loading/saving implementation based on your specific game world requirements and serialization preferences.


*/

using Newtonsoft.Json;
using System.IO;
using System;
using Windows.Storage;
using System.Diagnostics;

namespace MonolithEngine
{
    public class WorldClass
    {

        public WorldClass() { }

        /*
        In this updated implementation, we use the Windows.Storage API to access the file 
        in the UWP app's installed location. We first get the InstalledLocation 
        of the current package, then use GetFileAsync to get the file object. 
        Finally, we open the file stream for reading using OpenReadAsync.

        The rest of the code remains the same, where we use the StreamReader to read
        the game world data from the file and deserialize it into a GameWorld object. 
         */
        public async void Load(string filePath)
        {
            try
            {
                // Open the file stream for reading
                var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var file = await folder.GetFileAsync(filePath);
                var stream = await file.OpenReadAsync();

                using (var reader = new StreamReader(stream.AsStreamForRead()))
                {
                    // Read the game world data from the file
                    string json = await reader.ReadToEndAsync();

                    // Deserialize the JSON data into a GameWorld object
                    GameWorld gameWorld = JsonConvert.DeserializeObject<GameWorld>(json);

                    // Use the loaded game world data to set up your game scene
                    // ...
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during loading
                Debug.WriteLine("Error loading game world: " + ex.Message);
            }
        }

        /*
         In this example, we create a new GameWorld object and update its state 
        based on the current game scene. Then, we serialize the GameWorld object 
        to JSON using JsonConvert.SerializeObject.

        Next, we open the file stream for writing using CreateFileAsync and OpenAsync. 
        We use the StreamWriter to write the JSON data to the file.
        */

        public async void Save(string filePath)
        {
            try
            {
                // Create a new GameWorld object with the current game state
                GameWorld gameWorld = new GameWorld();
                // Update the game world state based on the current game scene

                // Serialize the GameWorld object to JSON
                string json = JsonConvert.SerializeObject(gameWorld);

                // Open the file stream for writing
                var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var file = await folder.CreateFileAsync(filePath, 
                    CreationCollisionOption.ReplaceExisting);
                var stream = await file.OpenAsync(FileAccessMode.ReadWrite);

                using (var writer = new StreamWriter(stream.AsStreamForWrite()))
                {
                    // Write the JSON data to the file
                    await writer.WriteAsync(json);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during saving
                Debug.WriteLine("Error saving game world: " + ex.Message);
            }
        }
    }
}
