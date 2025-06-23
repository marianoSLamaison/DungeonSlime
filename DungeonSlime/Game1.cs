using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace DungeonSlime;

public class Game1 : Core
{
    //private GraphicsDeviceManager _graphics;
    //private SpriteBatch _spriteBatch;
    private Texture2D _logo;
    public Game1() : base("DungeonSlime", 1280, 720, false)
    {
        //_graphics = new GraphicsDeviceManager(this);
        //Content.RootDirectory = "Content";
        //IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        //_spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        base.LoadContent();
        _logo = Content.Load<Texture2D>("images/logo");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
        // The bounds of the icon within the texture.
        Rectangle iconSourceRect = new Rectangle(0, 0, 128, 128);
        // The bounds of the word mark within the texture.
        Rectangle wordmarkSourceRect = new Rectangle(150, 34, 458, 58);
        SpriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        //Back to front toma el z index mas como estoy acostumbrado
        //(a mayor mas "lejos de la camara" esta el objeto) mientras que front to back lo toma al revez

        /*
        Luego esta el modo Texture que optimisa el dibujado al ordenar las llamadas por textura usada
        y luego esta instant que hace que en vez de mandar al buffer todo se mande directamente a la gpu
        puede generar problemas de endimineto, pero en ventaja esta el hecho de que puedes ajustar
        parametros para los shaders en cada llamada de dibujo
        */
        SpriteBatch.Draw(
            _logo,              //la textura
            new Vector2(
                Window.ClientBounds.Width,
                Window.ClientBounds.Height ) / 2,//la posicion en pantalla
            iconSourceRect,//el rectangulo de fuente ( si tienes un atlas de texturas supongo )
            Color.White,//tinte (blanco es igual a no tinte) Funciona multiplicando las componentes del color dado por las componentes
            //de los pixeles de la imagen dada
            0.0f,//rotacion (en radianes)
            new Vector2(
                iconSourceRect.Width,
                iconSourceRect.Height)/2,//origen desde donde se empieza a dibujar (imagina que primero defines el rectangulo
                //que va a contener tu imagen, entonces este parametro te dice dentro de ese rectangulo donde te posicionas para dibujar
                //como resultado, todas las otras transformaciones tambien se realizan con respecto de este punto)
                //los rectangulos aqui son con ints por que como es una textura, no tiene sentido usar floats ( esta dividida en pixeles que 
                //a su vez son indivisibles )
            1.0f,//escala ( si es un float la escala es igual para cada eje, caso contrario, puedes dar un vector)
            SpriteEffects.None,//efectos (es para saber si quieres renderizarla con efecto espejo de algun tipo o no)
            1.0f);//layer depth Como el z index degodot, aqui a mayor el numero mas cerca de pantalla esta ( solo funciona si 
                  //estas renderizando en modo back to front o front to back )
            // Draw only the word mark portion of the texture.
        SpriteBatch.Draw(
            _logo,              // texture
            new Vector2(        // position
            Window.ClientBounds.Width,
            Window.ClientBounds.Height) * 0.5f,
            wordmarkSourceRect, // sourceRectangle
            Color.White,        // color
            0.0f,               // rotation
            new Vector2(        // origin
            wordmarkSourceRect.Width,
            wordmarkSourceRect.Height) * 0.5f,
            1.0f,               // scale
            SpriteEffects.None, // effects
            0.0f                // layerDepth
        );
        SpriteBatch.End();
    }
}
