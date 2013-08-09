public static void Effects(Player player) 
	{
	player.magicDamage += 0.10f;
	}
	
public static void SetBonus(Player player)
	{
	player.setBonus = "+60 mana, +10% magic damage";
	player.statManaMax2 += 60;
	player.magicDamage += 0.10f;
	}