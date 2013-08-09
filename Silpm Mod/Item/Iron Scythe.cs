/* 		By Yoraiz0r
		Edited by Silpm		 */


ItemStyleSet Swing = new ItemStyleSet();
ItemStyleSet Shoot = new ItemStyleSet();
 
bool Shoot_KT = false;
bool Shoot_PTT = false;
 
public void Initialize()
{
    Swing.AnimationTime=25;
	Swing.UseStyle=1;
	Swing.UseTime=25;
	Swing.AutoReuse=true;
	Swing.UseSound=1;
	Swing.Mana=0;
	Swing.Damage=9;
	Swing.KB=4;
	Swing.Melee=true;
	
	Shoot.AnimationTime=25;
	Shoot.UseTime=25;
	Shoot.AutoReuse=false;
	Shoot.UseSound=9;
	Shoot.Mana=20;
	Shoot.Damage=23;
	Shoot.NoGFX=true;
	Shoot.ShootSpeed=11;
	Shoot.UseStyle=1;
	Shoot.Melee=true;
	//Shoot.Projectile="Test Scythe"; - doesn't work
	Shoot.Shoot=Config.projDefs.byName["Iron Scythe"].type; // works fine
}
 
public void HoldStyle(Player player)
{
    if (player.controlUseTile) // right click
		{
        Shoot_PTT = true;
        Shoot_KT = true;
		}else
        {
        Shoot_KT = false;
		Shoot_PTT = false;
		}
}
 
public void PreItemCheck(Player P)
{
    if(Shoot_PTT)
    {
        P.controlUseItem = true;
        Shoot.PasteItem(item);
        Shoot_PTT = false;
        return;
    }
    if(P.itemAnimation != 0) return;
    Swing.PasteItem(item);
}
 
public bool CanUse(Player Pr,int PID)
{
    int limit = 1;
 
    int counter = 0;
    int target = Shoot.Shoot;
    foreach (Projectile P in Main.projectile)
        {
                 if(P.active && P.owner == PID && P.type == target) counter++;
        }
    return counter<limit;
}
public class ItemStyleSet
{
    public int HoldStyle = 0;
    public int UseStyle = 0;
    public int Shoot = 0;
    public float ShootSpeed = 0;
    public int AmmoUse = 0;
    public int Damage = 0;
    public float KB = 0;
	public int Mana = 0;
	//public string Projectile = null;
	public bool AutoReuse = true;
 
    public int UseTime = 0;
    public int AnimationTime = 0;
 
    public int UseSound = 0;
 
    public bool Channel = false;
    public bool NoGFX = false;
    public bool NoMelee = false;
    public bool Melee = false;
    public bool Ranged = false;
    public bool Magic = false;
 
    public ItemStyleSet()
    {
    }
 
    public void PasteItem(Item I)
    {
        I.useStyle = UseStyle;
        I.holdStyle = HoldStyle;
		I.autoReuse = AutoReuse;
 
        I.shoot = Shoot;
        I.useAmmo = AmmoUse;
        I.shootSpeed = ShootSpeed;
		//I.projectile = Projectile;
		I.mana = Mana;
 
        I.damage = Damage;
        I.knockBack = KB;
 
        I.useTime = UseTime;
        I.useAnimation = AnimationTime;
 
        I.useSound = UseSound;
 
        I.channel = Channel;
 
        I.noUseGraphic = NoGFX;
        I.noMelee = NoMelee;
 
        I.melee=Melee;
        I.ranged=Ranged;
        I.magic=Magic;
    }
 
    public ItemStyleSet Clone()
    {
        return (ItemStyleSet)base.MemberwiseClone();
    }
}


public static void UseItemEffect(Player player, Rectangle rectangle) 
{
	Color color = new Color();
	int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, 15, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 100, color, 1.9f);
	Main.dust[dust].noGravity = true;
}