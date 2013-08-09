
ShotStyle Fast = new ShotStyle();
ShotStyle Slow = new ShotStyle();

int x=0;
bool fast = true;
bool slow = false;

public class ShotStyle
	{
	public bool AutoReuse = true;
	public int UseTime = 0;
	public int UseAnimation = 0;
	public int Damage = 0;
	
	public ShotStyle() {} // shit style
	
	public void PasteShotStyle(Item i) 
		{
		i.autoReuse = AutoReuse;
		i.useTime = UseTime;
		i.useAnimation = UseAnimation;
		i.damage = Damage;
		}
	}
	
public void Initialize()
	{
	Fast.AutoReuse=true;
	Fast.UseTime = 5;
	Fast.UseAnimation = 5;
	Fast.Damage = 24;
	
	Slow.AutoReuse=false;
	Slow.UseTime = 25;
	Slow.UseAnimation = 25;
	Slow.Damage = 70;
	}

public void HoldStyle(Player player)
	{
	x++;
	if (player.controlUseTile && x > 30)
		{
		x=0;
		if (fast)
			{
			fast=false;
			slow=true;
			Slow.PasteShotStyle(item);
			} else
			{
			fast=true;
			slow=false;
			Fast.PasteShotStyle(item);
			}
		}
	}