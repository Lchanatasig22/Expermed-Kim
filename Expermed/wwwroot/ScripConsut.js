var filaIndex = 1;
var medicamentoIndex = 1;
var laboratorioIndex = 1;
var imagenIndex = 1;


// Añadir nueva fila
$('#anadirFila').on('click', function () {
    var nuevaFila = `
                                                                            <tr>
                                                                                <td>
                                                                                    <div class="input-group">
                                                                                        <input type="text" class="form-control" placeholder="Buscar...">
                                                                                                 <div class="input-group">
                                                <select asp-for="DiagnosticoConsultaDiNavigation.IdDiagnostico" class="form-control" id="DiagnosticoId">
                                                    <option value="">Seleccione...</option>

                                                    @if (ViewBag.TiposDiagnostico != null)
                                                    {
                                                        @foreach (var tiposDiagnostico in ViewBag.TiposDiagnostico)
                                                        {
                                                            <option value="@tiposDiagnostico.Value">@tiposDiagnostico.Text</option>
                                                        }
                                                    }
                                                </select>
                                                <div class="input-group-append">
                                                    <span class="input-group-text"><i class="fas fa-search"></i></span>
                                                </div>
                                            </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="btn-group btn-group-toggle" data-toggle="buttons">
                                                                                        <label class="btn btn-outline-secondary">
                                                                                            <input type="radio" name="options${filaIndex}" id="presuntivo${filaIndex}" autocomplete="off"> Presuntivo
                                                                                        </label>
                                                                                        <label class="btn btn-outline-secondary">
                                                                                            <input type="radio" name="options${filaIndex}" id="definitivo${filaIndex}" autocomplete="off"> Definitivo
                                                                                        </label>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <button type="button" class="btn btn-outline-secondary eliminar-fila"><i class="fas fa-times-circle"></i> Eliminar</button>
                                                                                </td>
                                                                            </tr>`;
    $('#diagnosticoTableBody').append(nuevaFila);
    filaIndex++;
});

// Eliminar fila
$('#diagnosticoTableBody').on('click', '.eliminar-fila', function () {
    $(this).closest('tr').remove();
});

// Añadir nueva fila en Medicamentos
$('#anadirFilaMedicamento').on('click', function () {
    var nuevaFila = `
                                                                        <tr>
                                                                            <td>
                                                                                <div class="input-group">
                                                                                     <select asp-for="MedicamentoConsultaMNavigation.IdMedicamentosMedicamentoM" class="form-control" id="MedicamentoId">
                                                    <option value="">Seleccione...</option>

                                                    @if (ViewBag.TiposMedicamento != null)
                                                    {
                                                        @foreach (var tiposMedicamento in ViewBag.TiposMedicamento)
                                                        {
                                                            <option value="@tiposMedicamento.Value">@tiposMedicamento.Text</option>
                                                        }
                                                    }
                                                </select>
                                                                                    <div class="input-group-append">
                                                                                        <span class="input-group-text"><i class="fas fa-search"></i></span>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <input type="number" max="999" placeholder="0" class="form-control">
                                                                            </td>
                                                                            <td>
                                                                                <input type="text" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control">
                                                                            </td>
                                                                            <td>
                                                                                <button type="button" class="btn btn-outline-secondary eliminar-fila-medicamento"><i class="fas fa-times-circle"></i> Eliminar</button>
                                                                            </td>
                                                                        </tr>`;
    $('#medicamentosTableBody').append(nuevaFila);
    medicamentoIndex++;
});

// Eliminar fila en Medicamentos
$('#medicamentosTableBody').on('click', '.eliminar-fila-medicamento', function () {
    $(this).closest('tr').remove();
});

// Añadir nueva fila en Imágenes (Exámenes)
$('#anadirFilaImagen').on('click', function () {
    var nuevaFila = `
                                                            <tr>
                                                                <td>
                                                                    <div class="input-group">
                                                                        <input type="text" class="form-control" placeholder="Buscar...">
                                                                        <div class="input-group-append">
                                                                            <span class="input-group-text"><i class="fas fa-search"></i></span>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <input type="number" max="999" placeholder="0" class="form-control">
                                                                </td>
                                                                <td>
                                                                    <input type="text" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control">
                                                                </td>
                                                                <td>
                                                                    <button type="button" class="btn btn-outline-secondary eliminar-fila-imagen"><i class="fas fa-times-circle"></i> Eliminar</button>
                                                                </td>
                                                            </tr>`;
    $('#imagenesTableBody').append(nuevaFila);
    imagenIndex++;
});

// Eliminar fila en Imágenes (Exámenes)
$('#imagenesTableBody').on('click', '.eliminar-fila-imagen', function () {
    $(this).closest('tr').remove();
});

// Añadir nueva fila en Laboratorio
$('#anadirFilaLaboratorio').on('click', function () {
    var nuevaFila = `
                                                                    <tr>
                                                                        <td>
                                                                            <div class="input-group">
                                                                                <input type="text" class="form-control" placeholder="Buscar...">
                                                                                <div class="input-group-append">
                                                                                    <span class="input-group-text"><i class="fas fa-search"></i></span>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <input type="number" max="999" placeholder="0" class="form-control">
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" maxlength="300" placeholder="Máximo 300 caracteres" class="form-control">
                                                                        </td>
                                                                        <td>
                                                                            <button type="button" class="btn btn-outline-secondary eliminar-fila-laboratorio"><i class="fas fa-times-circle"></i> Eliminar</button>
                                                                        </td>
                                                                    </tr>`;
    $('#laboratorioTableBody').append(nuevaFila);
    laboratorioIndex++;
});

// Eliminar fila en Laboratorio
$('#laboratorioTableBody').on('click', '.eliminar-fila-laboratorio', function () {
    $(this).closest('tr').remove();
});


var navListItems = $('div.stepwizard-step button'),
    allWells = $('.setup-content');

allWells.hide();

// Manejo de clicks en los pasos del wizard
navListItems.click(function (e) {
    e.preventDefault();
    var $target = $('#step-' + $(this).data('step')),
        $item = $(this);

    if (!$item.hasClass('disabled')) {
        navListItems.removeClass('btn-primary').addClass('btn-secondary');
        $item.addClass('btn-primary');
        allWells.hide();
        $target.show();
        $target.find('input:eq(0)').focus();
    }
});

// Función para manejar el botón "Siguiente"
$('div.setup-content button.nextBtn').click(function () {
    var curStep = $(this).closest(".setup-content"),
        curStepBtn = curStep.attr("id"),
        nextStepWizard = $('div.stepwizard-step button[data-step="' + (parseInt(curStepBtn.split('-')[1]) + 1) + '"]'),
        curInputs = curStep.find("input[type='text'],input[type='url']"),
        isValid = true;

    $(".form-group").removeClass("has-error");
    for (var i = 0; i < curInputs.length; i++) {
        if (!curInputs[i].validity.valid) {
            isValid = false;
            $(curInputs[i]).closest(".form-group").addClass("has-error");
        }
    }

    if (isValid) {
        nextStepWizard.removeAttr('disabled').trigger('click');
    }
});

// Función para manejar el botón "Regresar"
$('div.setup-content button.previousBtn').click(function () {
    var curStep = $(this).closest(".setup-content"),
        curStepBtn = curStep.attr("id"),
        prevStepWizard = $('div.stepwizard-step button[data-step="' + (parseInt(curStepBtn.split('-')[1]) - 1) + '"]');

    navListItems.removeClass('btn-primary').addClass('btn-secondary');
    prevStepWizard.addClass('btn-primary');
    allWells.hide();
    $('#step-' + (parseInt(curStepBtn.split('-')[1]) - 1)).show();
});

// Mostrar u ocultar campos de observación al cambiar los switches
$('.consulta-antecedente-checked').change(function () {
    var isChecked = $(this).prop('checked');
    var $observacionField = $(this).closest('.fields').find('.consulta-antecedente-observacion');

    if (isChecked) {
        $observacionField.show(); // Mostrar el campo de observación
        $observacionField.find('input').removeAttr('disabled'); // Habilitar el input
    } else {
        $observacionField.hide(); // Ocultar el campo de observación
        $observacionField.find('input').attr('disabled', 'disabled'); // Deshabilitar el input
    }
});