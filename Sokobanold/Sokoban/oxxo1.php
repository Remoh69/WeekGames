

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>WEEKGAMES - OXXO</title>
<link rel="stylesheet" href="MasterPage.css" type="text/css">
<link rel="stylesheet" href="reset.css" type="text/css">



<script src="../js/ga.js" async="" type="text/javascript"></script><script type="text/javascript" src="../js/jquery.js"></script>
		<script type="text/javascript">
		<!--
		var unityObjectUrl = "http://webplayer.unity3d.com/download_webplayer-3.x/3.0/uo/UnityObject2.js";
		if (document.location.protocol == 'https:')
			unityObjectUrl = unityObjectUrl.replace("http://", "https://ssl-");
		document.write('<script type="text\/javascript" src="' + unityObjectUrl + '"><\/script>');
		-->
		</script><script type="text/javascript" src="../js/UnityObject2.js"></script>
		<script type="text/javascript">
		<!--
			var config = {
				width: 960, 
				height: 600,
				params: { enableDebugging:"1" }
				
			};
			var u = new UnityObject2(config);

			jQuery(function() {

				var $missingScreen = jQuery("#unityPlayer").find(".missing");
				var $brokenScreen = jQuery("#unityPlayer").find(".broken");
				$missingScreen.hide();
				$brokenScreen.hide();
				
				u.observeProgress(function (progress) {
					switch(progress.pluginStatus) {
						case "broken":
							$brokenScreen.find("a").click(function (e) {
								e.stopPropagation();
								e.preventDefault();
								u.installPlugin();
								return false;
							});
							$brokenScreen.show();
						break;
						case "missing":
							$missingScreen.find("a").click(function (e) {
								e.stopPropagation();
								e.preventDefault();
								u.installPlugin();
								return false;
							});
							$missingScreen.show();
						break;
						case "installed":
							$missingScreen.remove();
						break;
						case "first":
						break;
					}
				});
				u.initPlugin(jQuery("#unityPlayer")[0], "oxxo.unity3d");
			});
		-->
		</script>
		<style type="text/css">
		
		body {
			background-color: white;
			color: black;
			text-align: center;
		}
		a:link, a:visited {
			color: #000;
		}
		a:active, a:hover {
			color: #666;
		}
		p.header {
			font-size: small;
		}
		p.header span {
			font-weight: bold;
		}
		p.footer {
			font-size: x-small;
		}
		div.content {
			margin: auto;
			width: 960px;
		}
		div.broken,
		div.missing {
			margin: auto;
			position: relative;
			top: 50%;
			width: 193px;
		}
		div.broken a,
		div.missing a {
			height: 63px;
			position: relative;
			top: -31px;
		}
		div.broken img,
		div.missing img {
			border-width: 0px;
		}
		div.broken {
			display: none;
		}
		div#unityPlayer {
			cursor: default;
			height: 600px;
			width: 960px;
		}
		div#Like_Dislike{
			height: 100px;
			width: 960px;
			text-align: right;
		}
		
		</style>

</head>

<body class="XOox">
<div id="Logo"><center>
	  <a href="http://www.weekgames.djingarey.fr"><img src="logo_weekgames.png" width="572" height="166" /></a>
</center></div>

<div id="Social">
    <table width="30%" border="0" cellspacing="10px" cellpadding="10px">
  <tr>
    <td><div class="fb-share-button" data-href="http://www.weekgames.djingarey.fr/Oxxo/" data-layout="button"></div></td>
    <td><div class="g-plus" data-action="share" data-annotation="none" data-href="http://www.weekgames.djingarey.fr/Oxxo/"></div></td>
    <td><a href="https://twitter.com/share" class="twitter-share-button" data-url="http://www.weekgames.djingarey.fr/Oxxo/" data-count="none" data-hashtags="OXXO">Share on tweeter</a></td>
  </tr>
</table>
    </div>
    
<div>
  <table width="100%" border="0" cellspacing="0" cellpadding="10">
    <tr>
      <td align="center"  valign="top">
      <div class="content">
			<div id="unityPlayer" class="Box"><embed style="display: block; width: 960px; height: 600px;" enabledebugging="1" firstframecallback="UnityObject2.instances[0].firstFrameCallback();" type="application/vnd.unity" src="oxxo.unity3d" height="600" width="960"></div>
			<div id="Like_Dislike"><form name = "Like_Dislike" method = "post" action = "http://g.grousson38540.free.fr/Jeu_3D/Sokoban/oxxo.php"><?PHP echo $_POST['Like'];?><input type = "submit" name = "Plus" value = "Like"><input type = "submit" name = "Moins" value = "Dislike"><?PHP echo $_POST['DisLike'];?></form></div>
		</div>
      </td>
    </tr>
    <tr>
      <td align="center"  valign="top">&nbsp;</td>
    </tr>
  </table>

  <div id="Footer"><center> Mini-games made by Ga&euml;l Grosson and &Eacute;ric Djingarey. </center></div>
<!-- facebook -->
<div id="fb-root"></div>
<script>(function(d, s, id) {
  var js, fjs = d.getElementsByTagName(s)[0];
  if (d.getElementById(id)) return;
  js = d.createElement(s); js.id = id;
  js.src = "//connect.facebook.net/fr_FR/sdk.js#xfbml=1&version=v2.5&appId=533350613391216";
  fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
<!-- google + -->
<script type="text/javascript" src="https://apis.google.com/js/platform.js">
  {lang: 'fr'}
</script>
<!-- tweeter -->
<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+'://platform.twitter.com/widgets.js';fjs.parentNode.insertBefore(js,fjs);}}(document, 'script', 'twitter-wjs');</script>
</body>
</html>
