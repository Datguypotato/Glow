var io = require('socket.io')(process.env.PORT || 52300);
var Player = require('./Classes/Player');

console.log('Server has started');

var players = [];
var sockets = [];

io.on('connection', function(socket)
{
   console.log('User Connected')

   var player = new Player();
   var thisPlayerID = player.id;

   players[thisPlayerID] = player;
   sockets[thisPlayerID] = socket;

   console.log('The user ID: ' + thisPlayerID);
   // server to client
   socket.emit('register', {id: thisPlayerID})
   socket.emit('spawn', player) // tell myself i have spawned
   // send to all the other sockets except yourself
   socket.broadcast.emit('spawn', {id: thisPlayerID}) // tell others i have spawned

   // tell myself about everyone else
   for(var playerID in players)
   {
      if(playerID != thisPlayerID)
      {
         socket.emit('spawn', players[playerID]);
      }
   }

   // position data from client(Unity)   
   socket.on('updatePosition', function(data)
   {
      player.position.x = data.position.x;
      player.position.y = data.position.y;

      socket.broadcast.emit('updatePosition', player);
   });

   // client to server
   socket.on('disconnect', function(){
      console.log('a user has disconnected')
      delete players[thisPlayerID];
      delete socket[thisPlayerID];

      socket.broadcast.emit('disconnected', player)
   });
});





