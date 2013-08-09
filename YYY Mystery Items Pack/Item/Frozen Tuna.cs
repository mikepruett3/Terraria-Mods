

public void DamageNPC(Player P,NPC npc, ref int damage, ref float knockback) 
{
	int drunken = 25;
    for (int m = 0; m < 10; m++)
    {
        if (P.buffType[m] > 0 && P.buffTime[m] > 0)
        {
            if (P.buffType[m] == 25)	
            {
                drunken = 100;
            }
        }
    }
    damage=drunken;
}
