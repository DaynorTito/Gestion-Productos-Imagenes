<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\micontrolador;
/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

Route::get('/mivista', function () {
    return view('mivista');
});

Route::get('/mivistaparametros', function () {
	$myname="Moises Silva";
    return view('mivistaparametros', ['minombre'=>$myname]);
});

Route::get('/micontrolador/{myname}',[micontrolador::class,'index']);

Route::get('/micontroladorcito/bdatos',[micontrolador::class,'lista']);

Route::get('/micontroladorcito/edita/{ci}',[micontrolador::class,'edita']);
