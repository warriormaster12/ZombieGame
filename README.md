# ZombieGame
![image](https://github.com/warriormaster12/ZombieGame/assets/33091666/0617fef8-8bbc-4bbb-b780-1a169b28ebdb)

![image](https://github.com/warriormaster12/ZombieGame/assets/33091666/0e69271c-ddb4-411f-93d5-60296719a3ed)


## Game design docs
[Zombie survival - GDD.pdf](https://github.com/warriormaster12/ZombieGame/files/13219875/Zombie.survival.-.GDD.pdf)

[raw google doc](https://docs.google.com/document/d/1tly5SKjEC33qKjKrR0PIr41MdkYfB2KvHterO0fm3l4/edit?usp=sharing)

## Project management 
This is going to be handled in Github using built-in tools such as issues for tracking unimplemented features, bugs etc. On top of that, the project is going to use Github milestones for tracking the priority of resolving issues. 
Milestones are weekly based and the issues inside of them should be resolved inside the milestone unless it takes more or less time, in which case they can be moved between milestones.

## Coding conventions 
pascal case variables and functions 
camel case classes and structs

member variables are going to be marked with ```m_```
example: ```m_game_object```

## Code sample 
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_spawn_points;
    [SerializeField] private Vector2 m_spawn_interval = new Vector2(1.0f, 2.5f);

    [SerializeField] private int m_max_zombie_count = 10;

    private float timer = 0f;
    private int zombie_count = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        zombie_count = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if (m_spawn_points.Count == 0 || zombie_count == m_max_zombie_count) {
            timer = 0f;
            return;
        }

        timer += Time.fixedDeltaTime;
        if (timer >= Random.Range(m_spawn_interval.x, m_spawn_interval.y)) {
            int idx = Random.Range(0, m_spawn_points.Count);
            SpawnPoint spawn_point = m_spawn_points[idx].GetComponent<SpawnPoint>();
            if (!spawn_point) {
                Debug.LogError("No SpawnPoint component found");
                return;
            }
            spawn_point.SpawnZombie();
            timer = 0f;
        }
    }
}
```

## Itch.io
[link to the game](https://warriormaster12.itch.io/zombiegame)

### How to play
- wasd to move
- mouse cursor to look around
- shoot with left mouse button
- switch between machine gun and shotgun with scrollbar
