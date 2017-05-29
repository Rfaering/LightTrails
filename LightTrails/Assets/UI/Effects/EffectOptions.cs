using Assets.UI.Models;
public class EffectOptions
{
    public static Effect[] Options =
    {
          Effect.Create("Trails", typeof(TrailsMenuItem), loop: true),
          Effect.Create("Fireworks", typeof(FireworksMenuItem)),
          Effect.Create("Rain"),
          Effect.Create("SmallFire", typeof(SmallFireMenuItem), loop: true),
          Effect.Create("Fire", typeof(FireMenuItem)),
          Effect.Create("LargeFire"),
          Effect.Create("Wisps", typeof(WispMenuItem), loop: true),
          Effect.Create("Focus", typeof(FocusMenuItem)),
          Effect.Create("Dream"),
          Effect.Create("Twinkles", typeof(TwinklesMenuItem)),
          Effect.Create("Smoke"),
          Effect.Create("Snow", typeof(SnowMenuItem)),
          Effect.Create("Hearts", typeof(HeartsMenuItem), loop: true)
        };
}
