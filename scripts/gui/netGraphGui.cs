function execNetGraphGuiGUI()
{
   if ( isObject( NetGraphGui ) )
      NetGraphGui.delete();
 
   if ( isObject( NetGraphProfile ) )
      NetGraphProfile.delete();
           
   if ( isObject( NetGraphGhostsActiveProfile ) )
      NetGraphGhostsActiveProfile.delete();
      
   if ( isObject( NetGraphGhostUpdatesProfile ) )
      NetGraphGhostUpdatesProfile.delete();
   
   if ( isObject( NetGraphBitsSentProfile ) )
      NetGraphBitsSentProfile.delete();
      
   if ( isObject( NetGraphBitsReceivedProfile ) )
      NetGraphBitsReceivedProfile.delete();
      
   if ( isObject( NetGraphLatencyProfile ) )
      NetGraphLatencyProfile.delete();
      
   if ( isObject( NetGraphPacketLossProfile ) )
      NetGraphPacketLossProfile.delete();
      
   exec( "./NetGraphGui.gui" );  
}

// Profiles
new GuiControlProfile (NetGraphProfile)
{
   modal = false;
   opaque = false;
   canKeyFocus = false;
};

new GuiControlProfile (NetGraphKeyContainerProfile)
{
   border = true;
   opaque = true;
   fillColor = "100 100 100 200";
};
new GuiControlProfile (NetGraphGhostsActiveProfile)
{
   border = false;
   fontColor = "255 255 255";
};
new GuiControlProfile (NetGraphGhostUpdatesProfile)
{
   border = false;
   fontColor = "255 0 0";
};
new GuiControlProfile (NetGraphBitsSentProfile)
{
   border = false;
   fontColor = "0 255 0";
};
new GuiControlProfile (NetGraphBitsReceivedProfile)
{
   border = false;
   fontColor = "0 0 255";
};
new GuiControlProfile (NetGraphLatencyProfile)
{
   border = false;
   fontColor = "0 255 255";
};
new GuiControlProfile (NetGraphPacketLossProfile)
{
   border = false;
   fontColor = "0 0 0";
};


// Functions
function toggleNetGraph()
{
    if(!$NetGraph::isInitialized)
    {
        NetGraph::updateStats();
        $NetGraph::isInitialized = true;
    }

    if(!Canvas.isMember(NetGraphGui))
    {
        Canvas.add(NetGraphGui);
    }
    else
      Canvas.remove(NetGraphGui);
}

function NetGraph::updateStats()
{
  $NetGraphThread = NetGraph.schedule(32, "updateStats");

  if(!$Stats::netGhostUpdates)
     return;

  if(isobject(NetGraph))
  {
    if(isobject(ServerConnection))
        NetGraph.addDatum(0,ServerConnection.getGhostsActive());
    GhostsActive.setText("Ghosts Active: " @ ServerConnection.getGhostsActive());
    NetGraph.addDatum(1,$Stats::netGhostUpdates);
    GhostUpdates.setText("Ghost Updates: " @ $Stats::netGhostUpdates);
    NetGraph.addDatum(2,$Stats::netBitsSent);
    BitsSent.setText("Bytes Sent: " @ $Stats::netBitsSent);
    NetGraph.addDatum(3,$Stats::netBitsReceived);
    BitsReceived.setText("Bytes Received: " @ $Stats::netBitsReceived);
    NetGraph.matchScale(2,3);
    NetGraph.addDatum(4,ServerConnection.getPing());
    Latency.setText("Latency: " @ ServerConnection.getPing());
    NetGraph.addDatum(5,ServerConnection.getPacketLoss());
    PacketLoss.setText("Packet Loss: " @ ServerConnection.getPacketLoss());
  }
}

function NetGraph::toggleKey()
{
  if(!GhostsActive.visible)
  {
    GhostsActive.visible = 1;
    GhostUpdates.visible = 1;
    BitsSent.visible = 1;
    BitsReceived.visible = 1;
    Latency.visible = 1;
    PacketLoss.visible = 1;
  }
  else
  {
    GhostsActive.visible = 0;
    GhostUpdates.visible = 0;
    BitsSent.visible = 0;
    BitsReceived.visible = 0;
    Latency.visible = 0;
    PacketLoss.visible = 0;
  }
}
