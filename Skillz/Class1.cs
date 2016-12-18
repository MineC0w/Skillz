using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pirates;

namespace Skillz
{

    /* DEAR FUTURE READER, THIS IS 2016 YAIR: IF YOU ARE READING THIS, YOU MAY BE HAVING A PROBLEM CREATING A SMART PIRATE. REMEMBER YOU MAY HAVE TO PASS THE PIRATE BY REFERENCE. */

    public enum SPAWNPOINT { UNDETERMINED, LEFT, RIGHT };

    /* Improved implementation of a pirate */
    abstract class SmartPirate
    {

        private Pirate _pirate;
        private PirateGame _game;

        private LinkedListNode<Location> _currentMapTarget;
        private LinkedList<Location> _mapTargetBuffer;
        private bool _traveling;


        protected SmartPirate(Pirate pirate)
        {
            _pirate = pirate;
            

        }

        public virtual void DoTurn(PirateGame game) { }
        /* Execute a series of steps */
        public void GoTo(PirateGame game, LinkedList<Location> locationList)
        {
            if(_traveling)
            {
                /* SPirate is currently traveling */
                if(_pirate.GetLocation() == _currentMapTarget.Value)
                {
                    //Pirate reached destination
                    if (_currentMapTarget.Next == null)
                        _traveling = false;
                    else
                        _currentMapTarget = _currentMapTarget.Next;
                }
                else
                {
                    //Traveling but still hasn't reached destination
                    if(_pirate.IsAlive()) game.SetSail(_pirate,game.GetSailOptions(_pirate, _currentMapTarget.Value)[0]);


                }
            }
            else
            {
                //Not traveling, insert new list
                _mapTargetBuffer = locationList;

            }
        }

        public Aircraft FindClosestEnemy(bool prioritizeLowestHP = false, bool prioritizeShips = false)
        {
            Aircraft closest = null;
            int minDistance = 100000;

            foreach (Aircraft craft in _game.GetEnemyLivingAircrafts())
            {
                int dis = _pirate.Distance(craft);
                if(minDistance > dis)
                {
                    closest = craft;
                    minDistance = dis;
                } else if(minDistance==dis)
                {
                    if (closest != null)
                    {
                        if(prioritizeShips && craft.GetType() == typeof(Pirate))
                        {
                            // Prioritize this craft

                        } else if (closest.CurrentHealth > craft.CurrentHealth && prioritizeLowestHP)
                        {

                        }
                    }
                }
            }

            return closest;

        }

        public void FindClosestEnemy(Aircraft type)
        {

        }

    }

    /* A specific pirate role */
    class Camper : SmartPirate
    {
        public Camper(Pirate pirate) : base(pirate)
        {

        }

        public override void DoTurn(PirateGame game)
        {
            base.DoTurn(game);



        }
    }



   static class Empire
   {
        static SPAWNPOINT spawnpoint;
        public static void Init(PirateGame game)
        {
            spawnpoint = game.GetMyCities()[0] == game.GetAllCities()[0] ? SPAWNPOINT.LEFT : SPAWNPOINT.RIGHT;
        }
   }
}
