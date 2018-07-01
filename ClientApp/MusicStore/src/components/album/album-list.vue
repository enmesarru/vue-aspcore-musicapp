<template>
  <div>
    <el-row>
      <el-col :span="6" v-for="(o, index) in tableData" :key="index" :offset="index > 0 ? 2 : 0">
        <el-card class="cardac" :body-style="{ padding: '0px' }">
          <img v-bind:src="bindImage(o)" class="image">
          <span><time class="time">{{ o.releaseDate | formatDate }}</time></span>
          <div class="top-content">
            <el-button-group>
              <el-button size="mini" @click="deleteControl(o.id, index)"  type="danger" icon="el-icon-delete"></el-button>
              <router-link :to="{ name: 'album-edit' , params: {id: o.id}}">
                <el-button type="primary" size="mini">Edit</el-button>
              </router-link>
            </el-button-group>
          </div>
          <div style="padding: 5px;">
            <h4>{{o.name}}</h4>
            <div class="bottom clearfix">
              <span>Genre: {{o.genre}}</span> /
              <span>Artist Name: {{o.artist}}</span>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <router-link :to="{ name: 'album-create' }">
          <el-button type="primary" size="big" icon="el-icon-caret-right" >Create</el-button>
        </router-link>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'album-list',
      created () {
        this.fetchData()
      },
      data () {
        return {
          tableData: [],
          loading: false,
          BASE_URL: 'http://localhost:5000/api/'
        }
      },
      filters: {
        formatDate: (value) => {
          // return new Date(row.releaseDate).toDateString()
          return new Date(value).toISOString().slice(0, 10)
        }
      },
      methods: {
        bindImage (url) {
          return `http://localhost:5000/${url.albumArtURL}`
        },
        deleteControl (id, index) {
          this.$confirm('This will be permanently delete the album. Sure?', 'Warning', {
            confirmButtonText: 'OK',
            cancelButtonText: 'Cancel',
            type: 'warning'
          }).then(() => {
            this.deleteData(id, index)
            this.$message({
              type: 'success',
              message: 'Delete completed'
            })
          }).catch((error) => {
            this.$message({
              type: 'info',
              message: 'Delete canceled'
            })
            console.log(error)
          })
        },
        deleteData (id, index) {
          this.loadingStart()
          console.log(id, index)
          this.$http.delete(this.BASE_URL + `albums/${id}`)
            .then((data) => {
              console.log(data.body)
              this.tableData.splice(index, 1)
            }, (error) => {
              console.log(error)
            })
        },
        fetchData () {
          this.$http.get(this.BASE_URL + 'albums/')
            .then((response) => { this.tableData = response.body })
        },
        loadingStart () {
          this.loading = true
          setTimeout(() => {
            this.loading = false
          }, 2000)
        }
        /* formatDate (row) {
          // return new Date(row.releaseDate).toDateString()
          return new Date(row.releaseDate).toISOString().slice(0, 10)
        } */
      }
    }
</script>

<style scoped>
  .cardac {
    position: relative;
  }
  .time {
    font-size: 13px;
    color: #999;
    position: absolute;
    top: 0;
    left: 0;
    color: white;
    padding: 10px;
    margin:10px;
    background-color: #77d7b9;
    display: block;
  }
  .top-content {
    position: absolute;
    top: 0;
    right: 0;
    padding: 10px;
  }
  .bottom {
    margin-top: 13px;
    line-height: 12px;
  }

  .button {
    padding: 0;
    float: right;
  }

  .image {
    width: 100%;
    display: block;
  }

  .clearfix:before,
  .clearfix:after {
    display: table;
    content: "";
  }

  .clearfix:after {
    clear: both
  }
</style>
