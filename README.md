# Scp035
Scp035 is a mask which can spawn on the map and looks like a normal item. If a player picks it up a Spectator will become Scp-035 and take over the Player.

## RoleInformation
```
RoleID: 35
RoleName: Scp035
Team: Scp
```

## ItemInformation
Scp035 does for every vanilla item in the game add a scp035 item with the id + 100

Example for janitor Keycard
```
ItemID: 100 + 0 => 100
Name: Scp035-Item-KeycardJanitor
BasedItemType: KeycardJanitor
```

Example for Coin
```
ItemID: 100 + 35 => 135
Name: Scp035-Item-Coin
BasedItemType: Coin
```

## Config
| Config | Type | Description |
| :--: | :--: | :--: |
| Scp035Health | Integer | The Amount of Health Scp035 has |
| BadgeName | String | The Badge of Scp-035 |
| DeathTime | Boolean | If Enabled the Spectator with the highest death time will become Scp035 |
| Scp035ItemsAmount | Integer | The max Amount of Scp035 Items that can exist |
| Possible035Items | Integer List | The Items that the Scp035 mask can be |
| PickupSpawnInterval | Float | The interval in which the mask should change its position |
