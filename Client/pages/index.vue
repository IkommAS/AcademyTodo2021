<template>
  <div>
    <div id="myDIV" class="header">
      <h2 style="margin: 5px">My To Do List</h2>
      <input
        type="text"
        id="input"
        v-model="todoText"
        placeholder="What to do.."
      />
      <span @click="addTodo()" class="addBtn">Add</span>
    </div>
    <ul id="myUL">
      <li
        v-for="todo in getTodos"
        :key="todo.id"
        :class="{ checked: todo.isDone }"
        @click.self="toggleTodoDone(todo)"
      >
        {{ todo.name }}
        <span class="close" @click.self="deleteTodo(todo.id)">
          <a-icon style="pointer-events: none;" type="close-square" />
        </span>
      </li>
    </ul>
  </div>
</template>
<script>
import axios from "axios";
const baseUrl = "http://localhost:5000";
export default {
  mounted() {
    this.getAllTodos();
  },
  data() {
    return {
      todos: [],
      todoText: ""
    };
  },
  computed: {
    getTodos() {
      return this.todos;
    }
  },
  methods: {
    toggleTodoDone(todo) {
      todo.isDone = !todo.isDone;

      axios
        .put(baseUrl + "/api/Todo/" + todo.id, todo)
        .then(res => {
          console.log("updated todo successfully: ", res.data);
        })
        .catch(err => {
          console.log("set todo done failed: ", err);
        });
    },

    getAllTodos() {
      axios
        .get(baseUrl + "/api/Todo")
        .then(res => {
          console.log("getAllTodosInitially: ", res.data);
          this.todos = res.data;
        })
        .catch(err => {
          console.log("getAllTodos failed: ", err);
        });
    },

    deleteTodo(id) {
      id = parseInt(id);
      axios
        .delete(baseUrl + "/api/Todo/" + id)
        .then(res => {
          console.log("deleted todo successfully: ", res.data);
          this.todos = this.todos.filter(todo => todo.id !== id);
          console.log("todos", this.todos);
        })
        .catch(err => {
          console.log("delete todo failed: ", err);
        });
    },
    
    addTodo() {
      let obj = { name: this.todoText };
      console.log("addTodo req body: ", obj);
      axios
        .post(baseUrl + "/api/Todo", obj)
        .then(res => {
          console.log("todo added successfully: ", res.data);
          this.todos.push(res.data);
          this.todoText = "";
        })
        .catch(err => {
          console.log("add todo failed: ", err);
        });
    }
  }
};
</script>
<style>
body {
  margin: 0;
  min-width: 250px;
}
/* Include the padding and border in an elements total width and height */
* {
  box-sizing: border-box;
}
/* Remove margins and padding from the list */
ul {
  margin: 0;
  padding: 0;
}
/* Style the list items */
ul li {
  cursor: pointer;
  position: relative;
  padding: 12px 8px 12px 40px;
  list-style-type: none;
  background: #eee;
  font-size: 18px;
  transition: 0.2s; /* make the list items unselectable */
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}
/* Set all odd list items to a different color (zebra-stripes) */
ul li:nth-child(odd) {
  background: #f9f9f9;
}
/* Darker background-color on hover */
ul li:hover {
  background: #ddd;
}
/* When clicked on, add a background color and strike out text */
ul li.checked {
  background: #888;
  color: #fff;
  text-decoration: line-through;
}
/* Add a "checked" mark when clicked on */
ul li.checked::before {
  content: "";
  position: absolute;
  border-color: #fff;
  border-style: solid;
  border-width: 0 2px 2px 0;
  top: 10px;
  left: 16px;
  transform: rotate(45deg);
  height: 15px;
  width: 7px;
}
/* Style the close button */
.close {
  position: absolute;
  right: 0;
  top: 0;
  padding: 12px 16px 12px 16px;
}
.close:hover {
  background-color: #f44336;
  color: white;
}
/* Style the header */
.header {
  background-color: #26a2ae;
  padding: 30px 40px;
  color: white;
  text-align: center;
}
/* Clear floats after the header */
.header:after {
  content: "";
  display: table;
  clear: both;
}
/* Style the input */
input {
  margin: 0;
  border: none;
  border-radius: 0;
  width: 75%;
  padding: 10px;
  float: left;
  font-size: 16px;
}
/* Style the "Add" button */
.addBtn {
  padding: 10px;
  width: 25%;
  background: #d9d9d9;
  color: #555;
  float: left;
  text-align: center;
  font-size: 16px;
  cursor: pointer;
  transition: 0.3s;
  border-radius: 0;
}
.addBtn:hover {
  background-color: #bbb;
}
#input {
  color: black;
}
</style>
