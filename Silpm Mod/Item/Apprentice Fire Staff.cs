
ItemStyleSet Normal = new ItemStyleSet();
ItemStyleSet Bomb = new ItemStyleSet();

bool bomb = false;

public class ItemStyleSet
	{
	public int Mana = 0;
	public int Damage = 0;
	public bool AutoReuse = true;
	public int UseTime = 0;
	public int UseAnimation = 0;
	public int UseSound = 0;
	public int Shoot = 0;
	public int ShootSpeed = 0;
	
	public ItemStyleSet() { }
	
	public void PasteItem(Item I)
		{
		I.mana = Mana;
		I.damage = Damage;
		I.autoReuse = AutoReuse;
		I.useTime = UseTime;
		I.useAnimation = UseAnimation;
		I.useSound = UseSound;
		I.shoot = Shoot;
		I.shootSpeed = ShootSpeed;
		}
	}
	
public void Initialize()
	{
	Normal.Mana=15;
	Normal.Damage=45; //
	Normal.AutoReuse = true;
	Normal.UseTime = 45;
	Normal.UseAnimation = 45;
	Normal.UseSound = 9;
	Normal.Shoot = Config.projDefs.byName["Fire Staff Spirit"].type;
	Normal.ShootSpeed = 10;
	
	Bomb.Mana=50;
	Bomb.Damage=150; //
	Bomb.AutoReuse = false;
	Bomb.UseTime = 30;
	Bomb.UseAnimation = 30;
	Bomb.UseSound = 8;
	Bomb.Shoot = Config.projDefs.byName["App Fire Staff Bomb"].type;
	Bomb.ShootSpeed = 5;
	}
	
public void HoldStyle(Player player)
	{
	if (player.controlUseTile)
		{
		bomb = true;
		} else
		{
		bomb = false;
		}
	}
	
public void PreItemCheck(Player player)
	{
	if (bomb)
		{
		player.controlUseItem = true;
		Bomb.PasteItem(item);
		bomb = false;
		return;
		}
	if(player.itemAnimation!=0) return;
	Normal.PasteItem(item);
	}
		