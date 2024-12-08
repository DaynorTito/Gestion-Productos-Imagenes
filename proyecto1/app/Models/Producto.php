<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use App\Models\Categoria;
use App\Models\Unidad;
use App\Models\Lote;

class Producto extends Model
{

    protected $table = 'productos';
    protected $fillable = ['nombre', 'categoria_id', 'costo_produccion', 'unidad_medida_id', 'valor_unidad_medida'];
    public $timestamps = false;
    public function categoria()
    {
        return $this->belongsTo(Categoria::class, 'categoria_id');
    }

    public function unidad_medida()
    {
        return $this->belongsTo(Unidad::class, 'unidad_medida_id');
    }

    public function lotes()
    {
        return $this->hasMany(Lote::class);
    }
}
