<h1>Brute Force Attack 2</h1>

Brute Force Attack 2 is a tower defense game, in which your goal is to defend sensitive data from invading vira. At your disposal are component-based turret assemblies, that aims to provide near-infinite possibilities in terms of turret customizability. Additionally, you have several strategic structures, that may provide unique utilities or boosts to nearby turrets.

Brute Force Attack 2 is somewhere between a recreation of, and a sequel to the first Brute Force Attack. A recreation in the sense that this iteration aims to be what the first could never be, and a sequel in the sense that it extends and improves many things from the first game. The ultimate goal of Brute Force Attack 2 is to be a fun, interesting, and extendable tower defense game.

At the very core of the game lies a custom serialization/content system, that allows for the runtime loading of game content from customizable .json files. Content is compartmentalized into Content Packs that can easily be created and shared amongst players. The game is throughoutly written to be extendable and thus heavily moddable. While currently not possible, it is planned to allow the loading of custom C# assemblies as plugins into the game, allowing for infinite modding potential. See the [Core content pack here](https://github.com/Lomztein/Brute-Force-Attack-2/tree/master/Assets/StreamingAssets/Content/Core), or under StreamingAssets/Content in the game's data folder, as an example.

The content system allows for any part of the game to request content from any content pack, or all if a '\*' wildcard is used. Example:
 * `Content.Get("Core/Structures/Collector.json")` - Specifically loads the Collector
 * `Content.GetAll("Core/Components/")` - Loads all objects in the Core/Components folder.
 * `Content.GetAll("\*/Enemies/")` - Loads all objects in the Enemies folder of all content packs.
 
The game makes heavy use of this, for instance the wave generator uses the last example, in order to generate waves with enemies from all available content packs.

If you wish to create your own content, for instance with new enemies, you should thus place enemy-defining .json files in the Enemies folder. See the aforementioned Core content pack folder for other types of content. Unity AssetBundles and custom C# assemblies are planned to be supported as well.

This README will later be extended to include a guide to the basics of custom content creation, but for the time being, your best bet is to poke around and see what happens. Most content directly represent Unity GameObjects, and as such, familiarity with Unity may make it sagnificantly easier to figure out.

The Content loads objects at runtime using a Assembler/Model/Serializer system, wherein a Serializer takes in some JSON data, and deserializes it into a model. An assembler then takes the model and turns it into an in-game object. Both Assemblers and Serializers can also respectivly dissasemble and object into a model, and serialize a model into JSON data. This system is, as with the rest of the game, built to be expandable.
