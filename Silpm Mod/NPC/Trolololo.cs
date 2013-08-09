
public static bool SpawnNPC(int x, int y, int playerID) {
	if ( (ModWorld.CrazerKilled)&&(Main.rand.Next(250)==1)) {
		return true;
		}
		else{
	return false;}
}




public void NPCLoot()
{
    Gore.NewGore(npc.position,npc.velocity,"Trolololo Head",1f,-1);
}

