public static void UseItem(Player player, int playerID) {

			player.AddBuff("Gummiberry Juice", 18000, false);

}



public bool CanUse(Player player,int PlayerID)
{
	if (player.wings==0){
		return true;
		}
	else{
		Main.NewText("You can only use it without wings!");
		return false;
		}
	}