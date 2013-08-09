/*bool nospecialbiome = !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneHoly && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon; 
bool sky = nospecialbiome && ((double)y < Main.worldSurface * 0.44999998807907104); 
bool surface = nospecialbiome && !sky && (y <= Main.worldSurface); 
bool underground = nospecialbiome && !surface && (y <= Main.rockLayer); 
bool underworld= (y > Main.maxTilesY-190); 
bool cavern = nospecialbiome && !sky && !surface && !underground && !underworld && (y <= Main.rockLayer *25) && !Main.player[Main.myPlayer].zoneJungle; 
bool undergroundJungle = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneJungle; 
bool undergroundEvil = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneEvil; 
bool undergroundHoly = (y >= Main.rockLayer) && !underworld && (y <= Main.rockLayer *25) && Main.player[Main.myPlayer].zoneHoly; */



public static bool SpawnNPC(int x, int y, int playerID) {
	if (Main.player[playerID].townNPCs <= 0f && !Main.player[Main.myPlayer].zoneJungle && !Main.player[Main.myPlayer].zoneEvil && !Main.player[Main.myPlayer].zoneMeteor && !Main.player[Main.myPlayer].zoneDungeon
	&& (y <= Main.worldSurface) && ( (double) y > Main.worldSurface * 0.44999998807907104 ) && (Main.dayTime) && (ModWorld.CrazerKilled)
	) {
		if ( Main.player[Main.myPlayer].zoneHoly && Main.rand.Next(10)==1){
			return true;
			} else
		if ( Main.rand.Next(40)==1) {
			return true;
			}
			else return false;
		
		} else
	return false;
	}




public void NPCLoot()
{
    Gore.NewGore(npc.position,npc.velocity,"Gummi Head",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Gummi Leg",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Gummi Leg",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Gummi Arm",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Gummi Arm",1f,-1);
	Gore.NewGore(npc.position,npc.velocity,"Gummi Sword",1f,-1);
}

