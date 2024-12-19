<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <title>Laravel</title>

		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    </head>
    <body>
		<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
		
		<table class="table table-dark table-sm">

			<tr>
				<td>CI</td>
				<td>Nombres</td>
				<td>A.Paterno</td>
				<td>A.Materno</td>
				<td>Operaciones</td>
			</tr>

			@foreach($personas as $persona)
			<tr>
				<td>{{$persona->ci}}</td>
				<td>{{$persona->nombres}}</td>
				<td>{{$persona->apaterno}}</td>
				<td>{{$persona->amaterno}}</td>
				<td>
					<a href="/micontroladorcito/edita/{{$persona->ci}}">Editar</a>
					<a data-bs-toggle="modal" data-bs-target="#staticBackdrop" href="/micontroladorcito/edita/{{$persona->ci}}">Modal</a>
				</td>
			</tr>	
			@endforeach

		</table>
		
		<!-- Modal -->
		<div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
		  <div class="modal-dialog">
			<div class="modal-content">
			  <div class="modal-header">
				<h1 class="modal-title fs-5" id="staticBackdropLabel">Modal title</h1>
				<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
			  </div>
			  <div class="modal-body">
				...
			  </div>
			  <div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
				<button type="button" class="btn btn-primary">Understood</button>
			  </div>
			</div>
		  </div>
		</div>
		
    </body>
</html>
