bool trap = false;
int Xmult = 0;
int Ymult = 0;
int fuel = 0;
int fuelStep = 0;
bool Top;
public void Effects(Player player)
{
    player.doubleJump = true;
    player.canRocket = false;
    player.noFallDmg = true;
    player.rocketBoots = 2;
    if(player.whoAmi == Main.myPlayer)
    {
        if(player.wingTime > 0 || player.rocketTime > 0)
        {
            fuel = 20;
            //if(player.wingTime > 0 && player.wings > 0)
            //{
            //    fuel = fuel + player.wingTime;
            //}
            if(player.rocketTime > 0 && player.rocketBoots > 0)
            {
                fuel += player.rocketTime*10;
            }
            player.wingTime = 0;
            player.rocketTime = 0;
        }
        if(player.velocity.Y != 0f)
        {
            if(player.controlJump)
            {
                if(!trap)
                {    
                    trap = true;
                    Xmult = (player.controlRight?1:0) - (player.controlLeft?1:0);
                    Ymult = (player.controlDown?1:0) - (player.controlUp?1:0);
                    if(Xmult == 0 && Ymult == 0)
                        Ymult = -1;
                    if(Xmult != 0)
                        Ymult = 0;
                    fuelStep = 0;
                }
                if(player.jumpAgain && player.releaseJump)
                {
                    player.jumpAgain = false;
                }
                else if(!player.jumpAgain && fuel > 0)
                {
                    fuel--;
                    float Trickster = 0.00001f*(Top?1f:-1f);
                    Top = !Top;
                    Vector2 MovementOffset = new Vector2(10f*Xmult,10f*Ymult+Trickster-player.gravDir*0.498f);
                    player.velocity = MovementOffset;
                    if(fuelStep >= 6)
                    {
                        fuelStep = 0;
                        ModWorld.BoosterDust.NewBoosterDust(
                        "Booster Dust",
                        player.position+new Vector2(player.width/2,player.height/2),
                        player.velocity,
                        Vector2.Zero, 
                        20,
                        0f, 
                        1, 
                        16, 
                        16, 
                        1f
                        );
                    }
                    else
                    {
                        fuelStep++;
                    }
    
                }
                else
                {
                    
                } 
            }  
            else
            {
                if(trap && fuel > 0)
                    player.velocity = Vector2.Zero+new Vector2(0,0.0000001f);
                trap = false;
            } 
        }
        else
        {
        }
    }
}
