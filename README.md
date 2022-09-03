# Lua Table Serializer
Serialise/Deserialize C# data into a string representing a Lua Table


## Example

Code:
```c#
using LuaTableSerializer;

var lst = new List<string>(){"Hey", "You"};
var dict = new Dictionary<string, object>{
    {"Bob", 1},
    {"James", 1.1},
    {"Sid", "Sid"},
    {"list", lst},
    {"dict", new Dictionary<string, int>{
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        }
    },
    {"dictNumKey", new Dictionary<int, int>{
        {1, 1},
        {2, 2},
        {3, 3},
        {4, 4},
        }
    },
    {"dictOBJ", new Dictionary<string, object>{
        {"1", 1},
        {"1.5", 1.5},
        {"str", "item"},
        {"lst", lst},
        }
    }
};
var serialisedData = LuaSerializer.Serialize(dict);
Console.WriteLine($"mission = {serialisedData}");
var deserialised  =  LuaSerializer.Deserialize(serialisedData);
Utils.PrintDict(deserialised);

```

Output:

```lua
mission = {
        ["Bob"] = 1,
        ["James"] = 1.1,
        ["Sid"] = "Sid",
        ["list"] = {
                [1] = "Hey",
                [2] = "You",
                },
        ["dict"] = {
                ["1"] = 1,
                ["2"] = 2,
                ["3"] = 3,
                ["4"] = 4,
                },
        ["dictNumKey"] = {
                [1] = 1,
                [2] = 2,
                [3] = 3,
                [4] = 4,
                },
        ["dictOBJ"] = {
                ["1"] = 1,
                ["1.5"] = 1.5,
                ["str"] = "item",
                ["lst"] = {
                        [1] = "Hey",
                        [2] = "You",
                        },
                },
        }
// Print Dictionary as data.
K:Bob V:1
K:James V:1.1
K:Sid V:Sid
K:list V:
        K:1 V:Hey
        K:2 V:You
K:dict V:
        K:1 V:1
        K:2 V:2
        K:3 V:3
        K:4 V:4
K:dictNumKey V:
        K:1 V:1
        K:2 V:2
        K:3 V:3
        K:4 V:4
K:dictOBJ V:
        K:1 V:1
        K:1.5 V:1.5
        K:str V:item
        K:lst V:
                K:1 V:Hey
                K:2 V:You
```

## Classes

Classes can support serialization with the function `public string ToLuaString()`
This is just so you can output an class how you want and often incorporates a nested serialization

```
 public string ToLuaString()
 {
        var obj = newDictionary<string,string>{{"value",value}};
        return LuaSerializer.Serialize(obj);
 }
```
