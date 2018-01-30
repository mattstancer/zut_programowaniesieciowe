<?php
$base64_array=array("A","B","C","D","E","F","G","H","I","J","K", "L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
    "a","b","c","d","e","f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",  "r", "s", "t", "u", "v", "w",
    "x", "y","z", "0", "1", "2", "3", "4", "5","6", "7", "8", "9", "+","/");

$binarytable=array();
$handle = fopen('wstnc.png', "rb");
$contents = file_get_contents('wstnc.png');
$bytearray = array();
foreach(str_split($contents) as $char){
    array_push($bytearray, ord($char));
}
//print_r($array);

foreach ($bytearray as $byte)
{

    $bin= decbin($byte);
    $bin =str_pad($bin, 8, 0, STR_PAD_LEFT);
$binarka.= $bin;
        $i++;
    if($i==3){
        array_push($binarytable, $binarka);
        $i=0;
        $binarka='';
    }

}
$ilosc=count($bytearray);
//echo 'dupa: '.$ilosc.'<br/>';
if ($ilosc%3==1){
    $bin= decbin($bytearray[$ilosc-1]);
    $bin =str_pad($bin, 8, 0, STR_PAD_LEFT);
    array_push($binarytable, $bin);
}
else if ($ilosc%3==2){
    $bin1= decbin($bytearray[$ilosc-2]);
    $bin1 =str_pad($bin1, 8, 0, STR_PAD_LEFT);
    $bin= decbin($bytearray[$ilosc-1]);
    $bin =str_pad($bin, 8, 0, STR_PAD_LEFT);
    array_push($binarytable, $bin1.$bin);
}
//print_r($binarytable);
foreach($binarytable as $binarka1){

    if(strlen($binarka1)==24){
for( $i=0; $i<24; $i+=6){

$nowybit="00".substr($binarka1,$i,6);
//echo $base64_array[bindec($nowybit)];
$str=$str. $base64_array[bindec($nowybit)];

}}
    else if(strlen($binarka1)==16){
        $binarka1 =str_pad($binarka1, 24, 0, STR_PAD_RIGHT);
        for( $i=0; $i<18; $i+=6){

            $nowybit="00".substr($binarka1,$i,6);
            //echo $base64_array[bindec($nowybit)];
            $str=$str. $base64_array[bindec($nowybit)];

        }
        $str.='=';
    }
    else if(strlen($binarka1)==8){
        $binarka1 =str_pad($binarka1, 24, 0, STR_PAD_RIGHT);
        for( $i=0; $i<12; $i+=6){

            $nowybit="00".substr($binarka1,$i,6);
          //  echo $base64_array[bindec($nowybit)];
            $str=$str. $base64_array[bindec($nowybit)];

        }
        $str.="==";
    }
}
echo '<img src="data:image/png;base64,'.$str.'/>';
//echo '<br/>Base64 decode:'.base64_decode($str).'<br/>';
//echo base64_decode(base64_encode("ala ma kota\nkot"));
?>
