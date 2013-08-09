public int LitteralReference = 0;
public int LitteralTileReference = -1;
public int LitteralPlaceStyle = 0;
public void Initialize()
{
    this.LitteralReference = 0;
    this.LitteralTileReference = -1;
}

public bool PreDraw(SpriteBatch sp)
{
    SpriteEffects effects = SpriteEffects.None;
    if ((double)projectile.velocity.X > 0f)
    {
        projectile.direction = 1;
    }
    if ((double)projectile.velocity.X <0f)
    {
        projectile.direction = -1;
    }
    if (projectile.direction == -1)
    {
        projectile.spriteDirection = 1;
    }
    if (projectile.direction == 1)
    {
        projectile.spriteDirection = -1;
    }
    if (projectile.spriteDirection == 1)
    {
        effects = SpriteEffects.None;
    }
    else
    {
        effects = SpriteEffects.FlipHorizontally;
    }
    Color LitteralColor = Color.White;
    float Rotator = projectile.rotation;
    if(LitteralTileReference<0)
    {
        Rotator-=((float)Math.PI/4)*projectile.spriteDirection;
    }
    Texture2D mytex=Main.itemTexture[LitteralReference];
    sp.Draw(
    mytex, 
    projectile.position - Main.screenPosition, 
    new Rectangle?(new Rectangle(0,0, mytex.Width,mytex.Height)),
    Lighting.GetColor((int)projectile.position.X / 16, (int)(projectile.position.Y / 16f)), 
    Rotator, 
    new Vector2((float)mytex.Width * 0.5f, (float)mytex.Height * 0.5f),
    1f,
    effects, 
    0f);
    return false;
}

public void BeLitteral(int type,int Prefix)
{
    this.LitteralReference = type;
    Item ItemReference = new Item();
    ItemReference.SetDefaults(this.LitteralReference,false);
    ItemReference.Prefix(Prefix);
    if(ItemReference.createTile > -1)
    {
        LitteralTileReference = ItemReference.createTile;
        LitteralPlaceStyle = ItemReference.placeStyle;
    }
    projectile.width = ItemReference.width;
    projectile.height = ItemReference.height;
    projectile.width = (int)(projectile.width*1.4);
    projectile.height = (int)(projectile.height*0.6);
//    projectile.width = ItemReference.width;
//    projectile.height = ItemReference.height;
    projectile.damage = ItemReference.damage;
    projectile.knockBack = ItemReference.knockBack;
    projectile.melee = ItemReference.melee;
    projectile.ranged = ItemReference.ranged;
    projectile.magic = ItemReference.magic;
}
public void Kill()
{
    if(LitteralTileReference > -1)
    {
        Vector2 TileGrid = projectile.position+new Vector2(projectile.width/2,projectile.height/2);
        TileGrid/=16;
        int x = (int)TileGrid.X;
        int y = (int)TileGrid.Y;
        if(Main.tile[x,y] == null)
            Main.tile[x,y] = new Tile();
        if(false)
        {
        Main.tile[x,y].active = true;
        Main.tile[x,y].type = (byte)LitteralTileReference;
        WorldGen.SquareTileFrame(x, y, true);
        }
        else
        {
            WorldGen.PlaceTile(x, y, LitteralTileReference, false, false, projectile.owner, LitteralPlaceStyle);
        }
        projectile.active = false;
        return;
    }
    int TheSword = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, this.LitteralReference, 1, false, 0);
    Main.item[TheSword].noGrabDelay = 1;
    if (Main.netMode == 1)
    {
        NetMessage.SendData(21, -1, -1, "", TheSword, 0f, 0f, 0f, 0);
    }
    projectile.active = false;
}