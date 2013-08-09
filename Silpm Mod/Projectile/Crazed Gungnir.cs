        public void AI()
        {
            // All of this within this method is AI for a spear projectile, modified a bit from the source to apply to this custom spear
            // The finished result of this code gives my Gae Bolg Infernal (which is colored green) cursed flame colored dust upon attacking
            // for the dust types use this page on the wiki to fill in or change values http://tconfig.wikia.com/wiki/List_of_Dusts
            projectile.direction = Main.player[projectile.owner].direction;
            Main.player[projectile.owner].heldProj = projectile.whoAmI;
            Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
            projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
            projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);

            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 3f;
                projectile.netUpdate = true;
            }
            if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
            {
                projectile.ai[0] -= 2.4f;
            }
            else
            {
                projectile.ai[0] += 2.1f;
            }
            projectile.position += projectile.velocity * projectile.ai[0];
            if (Main.player[projectile.owner].itemAnimation == 0) // Has the projectile run its animation and returned to the player? If so kill it then...
            {
                projectile.Kill();
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
            Color newColor;
            if (Main.rand.Next(5) == 0)
            {
                Vector2 arg_54B5_0 = projectile.position;
                int arg_54B5_1 = projectile.width;
                int arg_54B5_2 = projectile.height;
                int arg_54B5_3 = 27; // this controls a dust type
                float arg_54B5_4 = 0f;
                float arg_54B5_5 = 0f;
                int arg_54B5_6 = 150;
                newColor = default(Color);
                Dust.NewDust(arg_54B5_0, arg_54B5_1, arg_54B5_2, arg_54B5_3, arg_54B5_4, arg_54B5_5, arg_54B5_6, newColor, 1.4f);
            }
            Vector2 arg_550C_0 = projectile.position;
            int arg_550C_1 = projectile.width;
            int arg_550C_2 = projectile.height;
            int arg_550C_3 = 27; // this controls a second dust type
            float arg_550C_4 = projectile.velocity.X * 0.2f + (float)(projectile.direction * 3);
            float arg_550C_5 = projectile.velocity.Y * 0.2f;
            int arg_550C_6 = 100;
            newColor = default(Color);
            int num116 = Dust.NewDust(arg_550C_0, arg_550C_1, arg_550C_2, arg_550C_3, arg_550C_4, arg_550C_5, arg_550C_6, newColor, 1.2f);
            Main.dust[num116].noGravity = true;
            Dust expr_552E_cp_0 = Main.dust[num116];
            expr_552E_cp_0.velocity.X = expr_552E_cp_0.velocity.X / 2f;
            Dust expr_554C_cp_0 = Main.dust[num116];
            expr_554C_cp_0.velocity.Y = expr_554C_cp_0.velocity.Y / 2f;
            Vector2 arg_55A4_0 = projectile.position - projectile.velocity * 2f;
            int arg_55A4_1 = projectile.width;
            int arg_55A4_2 = projectile.height;
            int arg_55A4_3 = 27; // this controls a third dust type 
            float arg_55A4_4 = 0f;
            float arg_55A4_5 = 0f;
            int arg_55A4_6 = 150;
            newColor = default(Color);
            num116 = Dust.NewDust(arg_55A4_0, arg_55A4_1, arg_55A4_2, arg_55A4_3, arg_55A4_4, arg_55A4_5, arg_55A4_6, newColor, 1.4f);
            Dust expr_55B8_cp_0 = Main.dust[num116];
            expr_55B8_cp_0.velocity.X = expr_55B8_cp_0.velocity.X / 5f;
            Dust expr_55D6_cp_0 = Main.dust[num116];
            expr_55D6_cp_0.velocity.Y = expr_55D6_cp_0.velocity.Y / 5f;
            return;
        }