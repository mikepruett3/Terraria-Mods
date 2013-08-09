public void UseItem(Player player, int playerID) { 
	Main.PlaySound(2,-1,-1,SoundHandler.soundID["Trolololo_kill"]);
    Main.NewText("Trololololololololololololo!");
	NPC.NewNPC((int)player.position.X,(int)player.position.Y,"Crazy Trolololo",0);
	ModWorld.CrazyTrolololoFight=true;
	Main.PlaySound(2,-1,-1,SoundHandler.soundID["TrolololoFull"]);
	Main.musicVolume=0f;
}