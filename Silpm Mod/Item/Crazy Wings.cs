public static void Effects(Player player)
{ 
	/*player.noFallDmg=true;
	if (player.controlJump && !player.controlDown){
		player.body=1;
		ModWorld.CrazyWingsFlap++;
		if (ModWorld.CrazyWingsFlap>=20){
			Main.PlaySound(2,-1,-1,32);
			ModWorld.CrazyWingsFlap=0;
		}
		if (player.velocity.Y>-9){player.velocity.Y-=1;}
		if (player.velocity.Y<-9){player.velocity.Y=-9;}
	}
	if (player.controlJump && player.controlDown){
		if (player.velocity.Y<2){player.velocity.Y+=3;}
		if (player.velocity.Y>2){player.velocity.Y=2;}
	}*/
	player.wings = ModWorld.WingIndex;
	player.wingTime = 99999999;
}