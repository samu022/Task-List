<template>
    <div>
        <h1 class="display-4 text-center">Listado de Tareas</h1>
        <hr>
        <div class="row">
            <div class="col-lg-8 offset-lg-2">
                <div class="card mt-4">
                    <div class="card-body">
                        <div class="input-group">
                            <input type="text" v-model="tarea"
                            class="form-control form-control-lg" placeholder="Agregar Tarea">
                            <div class="input-group-append">
                                <button v-on:click="agregarTarea()" 
                                class="btn btn-success btn-lg">
                                    Agregar
                                </button>
                            </div>        
                        </div>
                        <br />
                        <div class="text-center">
                             <div v-if="loading" class="spinner-border text-primary" role="status">
                                <span class="visually-hidden">Loading...</span>
                            </div>
                        </div>
                       
                        <h5 v-if="listTareas.length == 0">No hay tareas para realizar</h5>
                        <ul v-if="!loading" class="list-group">
                            <li v-for="(tarea, index) of listTareas" :key="index"
                             class="list-group-item d-flex justify-content-between">
                                <span class="cursor" v-bind:class="{'text-success': tarea.estado}"
                                v-on:click="editarTarea(tarea, tarea.id)">
                                    <i v-bind:class="[tarea.estado ? 'fa-solid fa-circle-check': 'fa-regular fa-circle' ]"></i>
                                    
                                </span>
                                {{ tarea.nombre }}
                                <span class="text-danger cursor" v-on:click="eliminarTarea(tarea.id)">
                                    <i class="fa-solid fa-trash"></i>
                                </span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            
        </div>
    </div>
</template>

<script>
import axios from "axios";
const URL ="https://localhost:7120/api/Tarea/";
    export default {
        name: 'TareaComponent',
        data() {
            return {
                tarea: '',
                listTareas: [],
                loading: false
            }
        },
        methods: {
            agregarTarea(){
                const tarea ={ 
                    nombre: this.tarea,
                    estado: false
                }
                //this.listTareas.push(tarea);
                this.loading=true;
                axios.post(URL, tarea).then(response => {
                    console.log(response);
                    this.loading=false;
                    this.obtenerTareas();
                }).catch(error => {
                    console.error(error);
                    this.loading=false;
                });
                this.tarea = ''
            },
            eliminarTarea(id){
                //this.listTareas.splice(index,1);
                this.loading=true;
                axios.delete(URL + id).then(response => {
                    console.log(response);
                    this.loading=false;
                    this.obtenerTareas();
                }).catch(error => {console.log(error);
                    this.loading=false;
                });
            },
            editarTarea(tarea, id){
                this.loading=true;
                //this.listTareas[index].estado=!tarea.estado;
                axios.put(URL + id, tarea).then(()=>{
                    this.loading=false;
                    this.obtenerTareas();
                }).catch(() => this.loading=false);
            },
            obtenerTareas(){
                this.loading=true;
                axios.get(URL).then(response => {
                    console.log(response);
                    this.loading=false;
                    this.listTareas=response.data;
                }).catch(()=> this.loading=false)
            }
        },
        created: function() {
            this.obtenerTareas();
        }
    }
</script>

<style scoped>
.cursor{
    cursor: pointer;
}
</style>