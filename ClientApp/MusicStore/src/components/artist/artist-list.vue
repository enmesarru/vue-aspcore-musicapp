<template>
  <div>
    <el-row>
      <el-col>
        <el-table
          v-loading="loading"
          :data="tableData"
          empty-text="List is busy"
          style="width: 100%">
          <el-table-column
            fixed
            prop="id"
            label="Id"
            width="150">
          </el-table-column>
          <el-table-column
            fixed
            prop="name"
            label="Name">
          </el-table-column>
          <el-table-column
            fixed
            prop="shortBio"
            label="Biography">
          </el-table-column>
          <el-table-column
            fixed="right"
            label="Operations"
            width="240">
            <template slot-scope="scope">
              <el-button-group>
                <el-button type="danger" @click="deleteControl(tableData[scope.$index].id, scope.$index)" size="small">Delete</el-button>
                <router-link :to="{ name: 'artist-edit' , params: {id: tableData[scope.$index].id}}">
                  <el-button type="primary" size="small">Edit</el-button>
                </router-link>
              </el-button-group>
            </template>
          </el-table-column>
        </el-table>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'artist-list',
      created () {
        this.fetchData()
      },
      methods: {
        deleteControl (id, index) {
          this.$confirm('This will be permanently delete the artist. Sure?', 'Warning', {
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
        fetchData () {
          this.loadingStart()
          this.$http.get(`http://localhost:5000/api/artists/`)
            .then((data) => {
              this.tableData = data.body
              console.log(data.body)
            }, data => {
              console.log(data.error)
            })
        },
        deleteData (id, index) {
          this.loadingStart()
          console.log(id, index)
          this.$http.delete(`http://localhost:5000/api/artists/${id}`)
            .then((data) => {
              console.log(data.body)
              this.tableData.splice(index, 1)
            }, (error) => {
              console.log(error)
            })
        },
        loadingStart () {
          this.loading = true
          setTimeout(() => {
            this.loading = false
          }, 2000)
        }
      },
      data () {
        return {
          loading: false,
          tableData: []
        }
      }
    }
</script>

<style scoped>

</style>
