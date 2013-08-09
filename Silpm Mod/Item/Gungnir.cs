/* 		Thx for Yoraiz0r
		This time it's 70% by me... or not... 
									~Silpm			*/
	
ItemDamageSet Normal = new ItemDamageSet();
ItemDamageSet JumpOrRun = new ItemDamageSet();
ItemDamageSet JumpAndRun = new ItemDamageSet();

bool jump = false;
bool run  = false;

	
public void Initialize()
	{
	Normal.Damage = 42;
	Normal.UseSound = 1;
	
	JumpOrRun.Damage = 52;
	JumpOrRun.UseSound = 1;
	
	JumpAndRun.Damage = 65;
	JumpAndRun.UseSound = 12;
	}
	
public void PreItemCheck(Player player)
	{
	if ( player.velocity.Y != 0 ) jump=true;
	if ( player.velocity.X > 5 || player.velocity.X < -5 ) run=true;
	
	if (jump && run) JumpAndRun.PasteSet(item); else
	if (jump || run) JumpOrRun.PasteSet(item); else
	Normal.PasteSet(item);
	
	jump=false;
	run=false;
	}
	
	
public class ItemDamageSet
	{
	public int Damage = 0;
	public int UseSound = 0;
	
	public ItemDamageSet() { }
	
	public void PasteSet(Item i)
		{
		i.damage = Damage;
		i.useSound = UseSound;
		}
	}