<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\micontrolador;
use App\Http\Controllers\CategoriaController;
use App\Http\Controllers\UnidadController;
use App\Http\Controllers\ProductoController;
use App\Http\Controllers\LoteController;


Route::get('/', function () {
    return view('inicio');
});

Route::resource('categorias', CategoriaController::class);
Route::resource('unidades', UnidadController::class);
Route::resource('productos', ProductoController::class);
Route::resource('lotes', LoteController::class);
