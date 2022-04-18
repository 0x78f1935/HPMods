### [Back to the index](./../../Devs.md)

# Layers found so far, their appearant use

| layer id | name                       | possible use                                           |
| -------- | -------------------------- | ------------------------------------------------------ |
| 0        | Default                    | colliders get placed here                              |
| 1        | TransparentFX              | --UNUSED--                                             |
| 2        | Ignore Raycast             | optimizations?                                         |
| 3        |                            | used by all furniture                                  |
| 4        | Water                      | --UNUSED--                                             |
| 5        | UI                         | UI Elements                                            |
| 6        |                            | --UNUSED--                                             |
| 7        |                            | --UNUSED--                                             |
| 8        | Floor                      | Floor, duh?                                            |
| 9        | Character                  | Characters bones reside here                           |
| 10       | Ragdolls                   | Characters ragdolls are in this layer                  |
| 11       | InteractiveItems           | well, the interactive items are in this layer          |
| 12       | NavAgents                  | --UNUSED--                                             |
| 13       | Trigger                    | probably for some trigger zones, unsure                |
| 14       | Mirror                     | was not loaded when tested, could be used              |
| 15       | Cursor                     | was not loaded when tested, could be used              |
| 16       | RagdollChest               | --UNUSED--                                             |
| 17       | Walls                      | all walls are part of this layer                       |
| 18       | InvisibleToMainCamera      | player head at least                                   |
| 19       | Penis                      | not sure if used                                       |
| 20       | HairColliders              | not sure if used                                       |
| 21       | Censor                     | the censor bar ios placed her, the mosaic probably too |
| 22       | VisionBlock                | blocks vision, mostly used for sex and alcohol         |
| 23       | LightIgnore                | presumably some optimizations?                         |
| 24       | PostProcess                | post process scripts                                   |
| 25       | RealTimeReflections        | reflections are her                                    |
| 26       | DirectionalIgnore          | --UNUSED--                                             |
| 27       | Camera                     | actually the camera is not in this layer but 0         |
| 28       | AudioCollider              | stops audio from propagating?                          |
| 29       | CharacterControllerBlocker | blocks the characters from moving there                |
| 30       | CharacterZoning            | contains the zones                                     |
| 31       | NavigateLineOfSightBlock   | micro optimizations for pathing                        |
| 32       |                            | --UNUSED--                                             |
