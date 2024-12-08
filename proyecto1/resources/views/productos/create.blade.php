<!-- resources/views/productos/create.blade.php -->
<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Crear Producto</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <a class="navbar-brand" href="/">Gestión de Productos</a>
        <div class="collapse navbar-collapse">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('categorias.index') ? 'text-white' : '' }}" href="{{ route('categorias.index') }}">Categorías</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('unidades.index') ? 'text-white' : '' }}" href="{{ route('unidades.index') }}">Unidades</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('productos.create') ? 'text-white' : '' }}" href="{{ route('productos.index') }}">Productos</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link {{ request()->routeIs('lotes.index') ? 'text-white' : '' }}" href="{{ route('lotes.index') }}">Lotes</a>
                </li>
            </ul>
        </div>
    </nav>
    <div class="container mt-4">
        <h2>Crear Nuevo Producto</h2>
        <form action="{{ route('productos.store') }}" method="POST">
            @csrf
            <div class="mb-3">
                <label for="nombre" class="form-label">Nombre</label>
                <input type="text" class="form-control" id="nombre" name="nombre" required>
            </div>
            <div class="mb-3">
                <label for="categoria_id" class="form-label">Categoría</label>
                <select class="form-select" id="categoria_id" name="categoria_id" required>
                    @foreach($categorias as $categoria)
                        <option value="{{ $categoria->id }}">{{ $categoria->nombre }}</option>
                    @endforeach
                </select>
            </div>
            <div class="mb-3">
                <label for="unidad_medida_id" class="form-label">Unidad de Medida</label>
                <select class="form-select" id="unidad_medida_id" name="unidad_medida_id" required>
                    @foreach($unidades as $unidad)
                        <option value="{{ $unidad->id }}">{{ $unidad->nombre }}</option>
                    @endforeach
                </select>
            </div>
            <div class="mb-3">
                <label for="valor_unidad_medida" class="form-label">Valor de Unidad de Medida</label>
                <input type="number" step="0.01" class="form-control" id="valor_unidad_medida" name="valor_unidad_medida" value="{{ old('valor_unidad_medida') }}" required>
            </div>
            <div class="mb-3">
                <label for="costo_produccion" class="form-label">Costo de Producción</label>
                <input type="number" class="form-control" id="costo_produccion" name="costo_produccion" required>
            </div>
            <button type="submit" class="btn btn-primary">Crear</button>
            <a href="{{ route('productos.index') }}" class="btn btn-secondary">Cancelar</a>
        </form>
    </div>
</body>
</html>
