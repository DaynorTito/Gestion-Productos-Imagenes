<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class micontrolador extends Controller
{
    // index
	public function index($minombre)
	{
		return "Hola ".$minombre;
	}
	
	public function lista()
	{
		$datos = DB::select("select * from persona");
		return view('mivistadatos')->with("personas", $datos);
	}
	
	public function edita($ci)
	{
		$datos = DB::select("select * from persona where ci='$ci'");
		return view('mivistaedita')->with("personas", $datos);
	}
	
	public function modificar(Request $request)
	{
		$datos = DB::insert("insert into persona(ci, nombbres, apaterno, amaterno) values(?, ?, ?, ?)", [$request->ci, $request->nombres, $request->apaterno, $request->amaterno]);
		if ($datos==true)
			return back()->with("Bien");
		else 
			return back()->with("Mal");
	}
}
