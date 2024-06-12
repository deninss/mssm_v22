$(document).ready(function () {
    var timer; // Переменная для хранения таймера
    $('#search-box').on('input', function () {
        var query = $(this).val();
        clearTimeout(timer);
        if (query.trim() === '') {
            $('#search-results').empty();
        }
        else {
            timer = setTimeout(function () {
                $.ajax({
                    url: '/Team/SearchTeam',
                    type: 'POST',
                    data: { query: query },
                    success: function (data) {
                        $('#search-results').html(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                        $('#search-results').html('<p>Ошибка при выполнении запроса</p>');
                    }
                });
            }, 500);
        }
    });
});
$(document).ready(function () {
    $('.addgroup-search div ul li').on('click', function (event) {
        var participantId = $(this).attr('id'); // Получаем ID элемента li, который был нажат
        var existingParticipant = $('#list-participants #' + participantId); // Ищем существующий клон по ID

        if (existingParticipant.length === 0) {
            var participantItem = $(this).clone(); // Клонируем элемент li 
            participantItem.find('button').addClass("active");
            participantItem.find('select').addClass("active");
            $('#list-participants').append(participantItem); 
        }
    });

    // Обработчик клика на кнопку удаления
    $(document).on('click', '.clear-participant', function (event) {
        event.stopPropagation(); // Предотвращаем всплытие события
        var participantItem = $(this).closest('li'); // Находим ближайший родительский элемент li
        participantItem.remove(); // Удаляем элемент li
 
    });
});
$(document).ready(function () {
    $('.tab-button').click(function () {
        var target = $(this).data('tab-target');
        $('.tab-button').removeClass('active');
        $('.tab-content').removeClass('active');
        $(this).addClass('active');
        $(target).addClass('active');
    });
});
$(document).ready(function () {
    $('#createBlockButton').click(function (event) {
        event.preventDefault();
        $.ajax({
            url: '/Project/AddItemProject',
            type: 'POST',
            success: function (result) {
                $('.content-add-item').append(result);
            }
        });
    });
});
$(document).on('click', '.add-task', function () {
    var projectId = $(this).closest('.new-Project').attr('id');
    // Отправляем AJAX-запрос на сервер
    $.ajax({
        url: '/Project/AddItemTask',
        type: 'POST',
        data: { projectId: projectId },
        success: function (response) {
            // Добавляем новую задачу в соответствующий new-Project
            $('#' + projectId).find('.container-project-footer').before(response);
        }
    });
});
$(document).on('click', '.task-delete', function () {
    // Находим ближайший new-Task к кнопке удаления и получаем его id
    var taskId = $(this).closest('.new-Task').attr('id');

    $.ajax({
        url: '/Project/DeleteItemTask', // Замените на ваш URL
        type: 'POST',
        data: { taskId: taskId }, // Передаем id задачи в теле запроса
        success: function (response) {
            // Удаляем задачу на клиентской стороне
            $('#' + taskId).remove();
        }
    });
});
$(document).on('click', '.project-delete', function () {
    // Находим ближайший new-Task к кнопке удаления и получаем его id
    var projectId = $(this).closest('.new-Project').attr('id');
    $.ajax({
        url: '/Project/DeleteProject', // Замените на ваш URL
        type: 'POST',
        data: { projectId: projectId }, // Передаем id задачи в теле запроса
        success: function (response) {
            // Удаляем задачу на клиентской стороне
            $('#' + projectId).remove();
        }
    });
});
$(document).on('click', '#task-subtasks', function () {
    var taskId = $(this).closest('.new-Task').attr('id');
    var subtasksCount = $('#' + taskId).find('.subtasks').children().length;
    if (subtasksCount < 2) {
        $.ajax({
            url: '/Project/AddItemSubtask',
            type: 'POST',
            data: { taskId: taskId }, 
            success: function (response) {
                $('#' + taskId).find('.subtasks').append(response);
            }
        });
    }
});
$(document).on('change', '#status-task', function () {
    var taskId = $(this).closest('.new-Task').attr('id');
    var selectedValue = $(this).find('option:selected').attr('id');

    console.log(taskId, selectedValue);
    $.ajax({
        url: '/Project/UrgencyTask',
        type: 'GET',
        data: { TaskId: taskId, selectedValue: selectedValue },
        success: function (response) {
            // Обработка успешного ответа
            console.log(response);
        },
        error: function (xhr, status, error) {
            console.error('Ошибка AJAX-запроса:', error);
        }
    });

});
    //$.ajax({
    //    url: '/Project/UrgencyTask',
    //    type: 'GET',
    //    data: { TaskId: taskId, selectedValue: selectedValue },
    //    success: function (response) {
    //        // Обработка успешного ответа
    //        console.log(response);
    //    },
    //    error: function (xhr, status, error) {
    //        console.error('Ошибка AJAX-запроса:', error);
    //    }
    //});

    // Set the selected option based on the value
    //$('#status-task option').filter(function () {
    //    return $(this).text() === selectedValue;
    //}).prop('selected', true);

$(document).on('click', '#urgency-task', function () {
    var $newTask = $(this).closest('.new-Task');
    var $itemUser = $newTask.find('.item-user div');
    var $select = $('<select id="status-task">');
    $select.append('<option id="1">important and urgent</option>');
    $select.append('<option id="2">important, but not urgent</option>');
    $select.append('<option id="3">urgent, but unimportant</option>');
    $select.append('<option id="4">non-urgent and unimportant</option>');
    $itemUser.append($select);
});
$(document).on('click', '#start-project', function () {
    var projectId = $(this).closest('.new-Project').attr('id');
    $.ajax({
        url: '/Project/StartProject', // Замените на ваш URL
        type: 'GET',
        data: { projectId: projectId }, // Передаем id задачи в теле запроса
        success: function (response) {
            // Удаляем задачу на клиентской стороне
          
        }
    });
});
$(document).on('click', '.task-subtasks-delete', function () {
    // Находим ближайший new-Task к кнопке удаления и получаем его id
    var subtaskId = $(this).closest('.container-subtasks').attr('id');
    $.ajax({
        url: '/Project/DeleteItemSubtasks', // Замените на ваш URL
        type: 'POST',
        data: { subtaskId: subtaskId }, // Передаем id задачи в теле запроса
        success: function (response) {
            // Удаляем задачу на клиентской стороне 
            $('#' + subtaskId).remove();
        }
    });
});
 
$(document).ready(function () {
    $(document).on("change", "#projectId, #projectName, #projectDescription, #start-date-project, #end-date-project", function () {
         var $projectElement = $(this).closest('.new-Project');
        var projectId = $projectElement.attr('id');
        var projectName = $projectElement.find("#projectName").val();
        var projectDescription = $projectElement.find("#projectDescription").val();
        var projectStartDate = new Date($projectElement.find("#start-date-project").val());
        var projectEndDate = new Date($projectElement.find("#end-date-project").val());

        clearTimeout($(this).data('timeout'));

        $(this).data('timeout', setTimeout(function () {
            $.ajax({
                url: '/Project/UpdateProject',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify({
                    Id: projectId,
                    Name: projectName,
                    Description: projectDescription,
                    StartDate: projectStartDate,
                    EndDate: projectEndDate
                }),
                success: function (response) {
                    console.log("Data sent to controller:", response);
                },
                error: function (error) {
                    console.error("Error sending data to controller:", error);
                }
            });
        }, 1000)); // Wait for 1 second after user stops typing
    });
     $(document).on("change", "#taskId, #taskName, #taskDescription, #start-date-task, #end-date-task", function () {
        var $taskElement = $(this).closest('.new-Task');
        var taskId = $taskElement.attr('id');
        var taskName = $taskElement.find("#taskName").val();
        var taskDescription = $taskElement.find("#taskDescription").val();
        var taskStartDate = new Date($taskElement.find("#start-date-task").val());
        var taskEndDate = new Date($taskElement.find("#end-date-task").val());

        clearTimeout($(this).data('timeout'));

        $(this).data('timeout', setTimeout(function () {
            $.ajax({
                url: '/Project/UpdateTask',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify({
                    Id: taskId,
                    Name: taskName,
                    Description: taskDescription,
                    IdExecutor: null,
                    File: null,
                    StartDate: taskStartDate,
                    EndDate: taskEndDate
                }),
                success: function (response) {
                    console.log("Data sent to controller:", response);
                },
                error: function (error) {
                    console.error("Error sending data to controller:", error);
                }
            });
        }, 1000)); // Wait for 1 second after user stops typing
    });


    $("#subtasksId, #subtasksDescription").on("input", function () {
        var $subtasksElement = $(this).closest('.container-subtasks');
        var subtasksId = $subtasksElement.attr('id');
        var subtasksDescription = $subtasksElement.find("#subtasksDescription").val();

        clearTimeout($(this).data('timeout'));

        $(this).data('timeout', setTimeout(function () {
            $.ajax({
                url: '/Project/UpdateSubtask',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify({
                    Id: subtasksId,
                    Description: subtasksDescription,
                    File: null,
                }),
                success: function (response) {
                    console.log("Data sent to controller:", response);
                },
                error: function (error) {
                    console.error("Error sending data to controller:", error);
                }
            });
        }, 1000)); // Wait for 1 second after user stops typing
    });

});
/// MODAL WINDOW
$(document).ready(function () {
    $(document).on('click', '.btn-add-team, .add-departments', function (e) {
        e.preventDefault();
        var controller = $(this).data('controller');
        var action = $(this).data('action');
        var Teamid = $(this).data('team'); 
        // Проверяем, что значения атрибутов не пустые
        if (controller && action) {
            $.ajax({
                url: '/' + controller + '/' + action,
                type: 'GET',
                data: {Teamid: Teamid},
                success: function (response) {
                    $('#my-modal .modal-window .content-modal').html(response);
                    $('#my-modal').addClass('show');
                },
                error: function (xhr, status, error) {
                    console.log("AJAX request failed: " + error);
                }
            });
        }
    });

    $(document).on('click', '.btn-close, .overlay', function () {
        $('#my-modal').removeClass('show');
    });
});
$(document).ready(function () {
    $(document).on('click', '#setting-project', function (e) {
        e.preventDefault();
        var controller = $(this).data('controller');
        var action = $(this).data('action');
        var projectId = $(this).closest('.new-Project').attr('id');  
        if (controller && action) {
            $.ajax({
                url: '/' + controller + '/' + action,
                type: 'GET',
                data: { projectId: projectId },
                success: function (response) {
                    $('#my-modal .modal-window .content-modal').html(response);
                    $('#my-modal').addClass('show');
                },
                error: function (xhr, status, error) {
                    console.log("AJAX request failed: " + error);
                }
            });
        }
    });

    $(document).on('click', '.btn-close, .overlay', function () {
        $('#my-modal').removeClass('show');
    });
});
$(document).on('click', '.select-btn-task', function () {
    $(this).closest('.select-menu-task').toggleClass("active");
}).on('mouseleave', '.select-btn-task, .select-menu-task', function () {
    var $selectMenu = $('.select-menu-task.active');
    setTimeout(function () {
        if (!$selectMenu.is(':hover')) {
            $selectMenu.removeClass("active");
        }
    }, 2000);
});
$(document).on('click', '.select-btn', function () {
    $(this).closest('.select-menu').toggleClass("active");
}).on('mouseleave', '.select-btn, .select-menu', function () {
        var $selectMenu = $('.select-menu.active');
        setTimeout(function () {
            if (!$selectMenu.is(':hover')) {
                $selectMenu.removeClass("active");
            }
        }, 2000);
    });
 
$(document).on('click', '#select-btn-department', function () {
    $(this).closest('.options').find('.option-Executor').toggleClass("active");
}).on('mouseleave', '#select-btn-department, .option-Executor', function () {
    var $selectMenu = $('.option-Executor.active');
    setTimeout(function () {
        if (!$selectMenu.is(':hover')) {
            $selectMenu.removeClass("active");
        }
    }, 2000);
});
$(document).ready(function () {
    $('.Executor').click(function () {
        var departmentId = $(this).data('department-id');
        //$('#container-executor-' + departmentId).toggle();
        $(this).closest('.options').find('#container-executor-' + departmentId).toggleClass("active");
    });
}).on('mouseleave', '.Executor, .container-executor', function () {
    var $selectMenu = $('.container-executor.active');
    setTimeout(function () {
        if (!$selectMenu.is(':hover')) {
            $selectMenu.removeClass("active");
        }
    }, 2000);
}); 
$(document).on('click', '.container-executor li', function (e) {
    e.preventDefault(); // Предотвращаем стандартное действие ссылки
    var ExecutorId = $(this).data('id'); // Получаем id из data-атрибута
    if ($(this).hasClass('executor-in-task')) {
        var TaskId = $(this).closest('.new-Task').attr('id');
        var Delete = true;
    } else {
        var TaskId = $(this).closest('.new-Task').attr('id');
        var Delete = false;
    }
 
    $.ajax({
        url: '/Project/SaveExecutor', // URL действия контроллера с параметром id в строке запроса
        type: 'GET',
        data: { ExecutorId: ExecutorId, TaskId: TaskId, Delete: Delete }, // Запятая была лишней
        success: function (result) {
            if (result.success) {
                if (!Delete) {
                    console.log($(this).closest('.new-Task').find('.item-user'));
                    $(this).closest('.new-Task').find('.item-user').before('<img class="input-img" src="~/Img/user.svg" />');
                }
                else {

                }
            }
        }
    });
});
$(document).on('click', '.team-link', function (e) {
        e.preventDefault(); // Предотвращаем стандартное действие ссылки
        var id = $(this).data('id'); // Получаем id из data-атрибута
        $.ajax({
            url: '/Team/ItemTeam?id=' + id, // URL действия контроллера с параметром id в строке запроса
            type: 'GET', // Метод должен быть доступен через GET
            success: function (result) {
                $('#tabContentTeam').html(result); // Вставляем результат в div
            }
        });
});
$(document).on('click', '#end-task', function (e) {
    e.preventDefault(); 
    var $newTask = $(this).closest('.item-task');
    var TaskId = $newTask.attr('id');
    $.ajax({
        url: '/Task/EndTask', // URL действия контроллера с параметром id в строке запроса
        type: 'GET',
        data: { TaskId: TaskId },
        success: function (result) {
            if (result.success) {
                $newTask.css('border-top', '5px solid #00daff');
            }
        }
    });
});
$(document).on('click', '#ok-task', function (e) {
    e.preventDefault();
    var $newTask = $(this).closest('.new-Task');
    var TaskId = $newTask.attr('id');
    $.ajax({
        url: '/Project/FinalTask', // URL действия контроллера с параметром id в строке запроса
        type: 'GET',
        data: { TaskId: TaskId },
        success: function (result) {
            if (result.success) {
                $newTask.css('border-top', '5px solid #0eff00');
                $newTask.find('#ok-task').attr('id', 'no-task'); // Изме
                $newTask.find('#no-task span.option-text').text('Отменить');
            }
        }
    });
});
$(document).on('click', '#no-task', function (e) {
    e.preventDefault(); 
    var $newTask = $(this).closest('.new-Task');
    var TaskId = $newTask.attr('id');
    $.ajax({
        url: '/Project/NoTask', // URL действия контроллера с параметром id в строке запроса
        type: 'GET',
        data: { TaskId: TaskId },
        success: function (result) { 
            if (result.success) {
                $newTask.css('border-top', '5px solid #00daff');
                $newTask.find('#no-task').attr('id', 'ok-task'); // Изме
                $newTask.find('#ok-task span.option-text').text('Завершить');
            }
        }
    });
}); 
$(document).ready(function () {
    var timer; // Переменная для хранения таймера

    $('#search-box-Departments').on('input', function () {
        var query = $(this).val();
        var TeamID = $(this).data('team');
        // Очищаем предыдущий таймер
        clearTimeout(timer);

        if (query.trim() === '') {
            // Если поле поиска пустое, удаляем содержимое div с результатами поиска
            $('#search-results-Departments').empty();
        }
        else {
            // Устанавливаем таймер, который отправит запрос через 700 миллисекунд после остановки ввода
            timer = setTimeout(function () {
                $.ajax({
                    url: '/Team/SearchDepartment',
                    type: 'GET',
                    data: { TeamID: TeamID, query: query },
                    success: function (data) {
                        $('#search-results-Departments').html(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                        $('#search-results-Departments').html('<p>Ошибка при выполнении запроса</p>');
                    }
                });
            }, 500);
        }
    });
});
$(document).ready(function () {
    // Обработчик клика на элементы списка
    $('.addgroup-search div ul li').on('click', function (event) {
        var participantId = $(this).attr('id');
        var existingParticipant = $('#list-participants-Departments #' + participantId);
        if (existingParticipant.length === 0) {
            var participantItem = $(this).clone(); 
            participantItem.find('button').addClass("active");
            $('#list-participants-Departments').append(participantItem);

        }
    });

    $(document).on('click', '.clear-participant', function (event) {
        event.stopPropagation(); 
        var participantItem = $(this).closest('li'); 
        participantItem.remove(); 
    });
});
 
function netTeam(data) {
    if (data.success) {
        var newListItem = '<li class="item-team">' +
            '<a href="#" class="team-link" data-id="' + data.newTeamsId + '">' +
            '<span class="text">' + data.newTeamsName + '</span>' +
            '</a>' +
            '</li>';
        // Вставка нового элемента в <ul>
        $('#modal-trigger').before(newListItem);
    } 
}
function netDepartment(data) {
    if (data.success) {
        var newListItem = '<input type="button" id="'+data.newDepId+'" class="departments" value="Отдел: '+data.newDepName+'"/>';
        // Вставка нового элемента в <ul>
        $('#modal-trigger-departments').before(newListItem);
    } 
} 
$(document).ready(function () {
    var timer; // Переменная для хранения таймера 
    $(document).on('input', '#search-team-staff-user', function () {
        var query = $(this).val();
        var TeamId = $(this).data('staff'); 
        // Очищаем предыдущий таймер
        clearTimeout(timer);

        if (query.trim() === '') {
            // Если поле поиска пустое, удаляем содержимое div с результатами поиска
            $('#team-member-list').empty();
        }
        else {
            // Устанавливаем таймер, который отправит запрос через 700 миллисекунд после остановки ввода
           
            timer = setTimeout(function () {
                $.ajax({
                    url: '/Team/SearchTeamStaff',
                    type: 'POST',
                    data: { query: query, TeamId: TeamId },
                    success: function (data) {
                        $('#team-member-list').html(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                        $('#team-member-list').html('<p>Ошибка при выполнении запроса</p>');
                    }
                });
            }, 500);
        }
    });
});
$('#add-user-team-staff').on('click', function (event) {
    event.preventDefault(); // Предотвращает стандартное действие кнопки
    var UserId;
    var Role;
    $("#team-member-list li").each(function () {
        UserId = $(this).attr("id");
        Role = $(this).find("select.role-participant").val();
    });
    var TeamId = $("#search-team-staff-user").data('staff');  // Assuming ViewBag.IdTeam is set on the search box
    $.ajax({
        type: "POST",
        url: "/Team/AddTeamStaff",
        data: { TeamId: TeamId, UserId: UserId, Role: Role },
        success: function (data) {
            
        },
        error: function (xhr, status, error) {
            // Обработка ошибки
        }
    });
});
$(document).ready(function () {
    var timer; // Переменная для хранения таймера 
    $(document).on('input', '#search-department-staff-user', function () {
        var query = $(this).val();
        var TeamId = $(this).data('staff');
        // Очищаем предыдущий таймер
        clearTimeout(timer);

        if (query.trim() === '') {
            // Если поле поиска пустое, удаляем содержимое div с результатами поиска
            $('#department-member-list').empty();
        }
        else {
            // Устанавливаем таймер, который отправит запрос через 700 миллисекунд после остановки ввода

            timer = setTimeout(function () {
                $.ajax({
                    url: '/Team/SearchDepartmentStaff',
                    type: 'POST',
                    data: { query: query, TeamId: TeamId },
                    success: function (data) {
                        $('#department-member-list').html(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                        $('#department-member-list').html('<p>Ошибка при выполнении запроса</p>');
                    }
                });
            }, 500);
        }
    });
});
$('#add-user-team-staff').on('click', function (event) {
    event.preventDefault(); // Предотвращает стандартное действие кнопки
    var UserId;
    var Role;
    $("#department-member-list li").each(function () {
        UserId = $(this).attr("id");
        Role = $(this).find("select.role-participant").val();
    });
    var TeamId = $("#search-department-staff-user").data('staff');  // Assuming ViewBag.IdTeam is set on the search box
    console.log(TeamId, UserId, Role); // Выводит значение TeamId в консоль браузера
    $.ajax({
        type: "POST",
        url: "/Team/AddDepartmentStaff",
        data: { TeamId: TeamId, UserId: UserId, Role: Role },
        success: function (data) {

        },
        error: function (xhr, status, error) {
            // Обработка ошибки
        }
    });
});
$(document).on('click', '#clear-team-staff', function (event) {
    event.stopPropagation(); // Предотвращаем всплытие события
    var liElement = $(this).closest('li');
    var UserId = liElement.attr('id'); // Получаем ID пользователя из атрибута id
     
    $.ajax({
        type: "POST",
        url: "/Team/DeleteTeamStaff",
        data: { UserId: UserId },
        success: function (data) {
            if (data.success) liElement.remove();
            else { }
        },
        error: function (xhr, status, error) {
            // Обработка ошибки
        }
    });
});
 
$(document).on('click', '#clear-department-staff', function (event) {
    event.stopPropagation(); // Предотвращаем всплытие события
    var liElement = $(this).closest('li');
    var UserId = liElement.attr('id'); // Получаем ID пользователя из атрибута id
     
    $.ajax({
        type: "POST",
        url: "/Team/DeleteDepartmentStaff",
        data: { UserId: UserId },
        success: function (data) {
            if (data.success) liElement.remove();
            else { }
        },
        error: function (xhr, status, error) {
            // Обработка ошибки
        }
    });
}); 

function updateTeamMemberList() {
    // Здесь можно вызвать AJAX запрос для обновления списка пользователей
    // Например:
    var id = $("#search-team-staff").data('staff');
    $.ajax({
        url: '/Team/ItemTeam?id=' + id,
        type: 'GET',
        success: function (data) {
            $('#tabContentTeam').html(data);
        },
        error: function (xhr, status, error) {
            console.log(error);
            $('#team-member-list').html('<p>Ошибка при выполнении запроса</p>');
        }
    });
}
function updateDepartmentMemberList() {
    var id = $("#search-department-staff").data('staff');
    $.ajax({
        type: "GET",
        url: "/Team/GetDepartment",
        data: { id: id },
        success: function (data) {
            $('#tabContentTeam').html(data);
        },
        error: function (xhr, status, error) {
            console.error('Ошибка при обновлении списка участников отдела:', error);
        }
    });
}
$(document).ready(function () {
    $(document).on('click', '.departments', function (e) {
        var id = $(this).attr('id');
       
        $.ajax({
            type: "GET",
            url: "/Team/GetDepartment",
            data: { id: id },
            success: function (data) {
                $('#tabContentTeam').html(data);
            },
            error: function (xhr, status, error) {
                // Обработка ошибки
            }
        });
    });
});
$(document).ready(function () {
    $(document).on('click', '#delete-department', function (e) {
        var DepartmentId = $(this).data('delete');
        $.ajax({
            type: "GET",
            url: "/Team/DeleteDepartment",
            data: { DepartmentId: DepartmentId },
            success: function (data) {
                   $('#tabContentTeam').html(data);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
});
$(document).ready(function () {
    $(document).on('click', '#delete-team', function (e) {
        var TeamId = $(this).data('delete');
        $.ajax({
            type: "GET",
            url: "/Team/DeleteTeam",
            data: { TeamId: TeamId },
            success: function (data) {
                if (data.success) {
                    $('li.item-team').find('a[data-id="' + TeamId + '"]').closest('li').remove();
                    $('#tabContentTeam').empty();
                }
          /*     *//* *//*$('#tabContentTeam').html(data);*/
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
});
//$(document).ready(function () {
//    $(document).on('click', '.option#date-project', function (e) {
//        var newProject = $(this).closest('.new-Project');
//        var contentND = newProject.find('.content-ND');
//        contentND.append('<div class="input-group-project-date"><input type="date" class="input-date" id="start-date-project" name="start-date"><input type="date" class="input-date" id="end-date-project" name="end-date"></div>');
//    });
//});
$(document).ready(function () {
    $(document).on('click', '.option#date-project', function (e) {
        e.preventDefault(); // Предотвращаем стандартное действие ссылки
        var newProject = $(this).closest('.new-Project');
        var contentTND = newProject.find('.content-ND');

        // Проверяем, существует ли уже элемент с классом input-group-project-date в content-TND
        if (contentTND.find('.input-group-project-date').length > 0) {
            // Если элемент уже существует, прекращаем выполнение функции
            return;
        }

        // Если элемента нет, добавляем новые
        contentTND.append('<div class="input-group-project-date"><input type="date" class="input-date" id="start-date-project" name="start-date"><input type="date" class="input-date" id="end-date-project" name="end-date"></div>');
    });
});
$(document).ready(function () {
    $(document).on('click', '.option#date-task', function (e) {
        e.preventDefault(); // Предотвращаем стандартное действие ссылки
        var newProject = $(this).closest('.new-Task');
        var contentTND = newProject.find('.content-TND');

        // Проверяем, существует ли уже элемент с классом input-group-project-date в content-TND
        if (contentTND.find('.input-group-project-date').length > 0) {
            // Если элемент уже существует, прекращаем выполнение функции
            return;
        }

        // Если элемента нет, добавляем новые
        contentTND.append('<div class="input-group-project-date"><input type="date" id="start-date-task" class="input-date"><input type="date" id="end-date-task" class="input-date"></div>');
    });
});
function updateTaskDates() {
    // Находим ближайший родительский элемент с классом new-Project
    var newProject = $(this).closest('.new-Project');
    // Находим выбранные даты начала и окончания проекта внутри этого new-Project
    var startDate = newProject.find('#start-date-project').val();
    var endDate = newProject.find('#end-date-project').val();
    // Находим все поля даты начала и окончания задач внутри этого new-Project
    var taskStartDates = newProject.find('.task-start-date');
    var taskEndDates = newProject.find('.task-end-date');

    // Обновление атрибутов min у полей даты начала задач
    taskStartDates.each(function () {
        $(this).attr('min', startDate);
    });

    // Обновление атрибутов max у полей даты окончания задач
    taskEndDates.each(function () {
        $(this).attr('max', endDate);
    });
}
// Привязываем функцию к событиям изменения значения полей даты
$(document).on('change', '#start-date, #end-date', updateTaskDates);