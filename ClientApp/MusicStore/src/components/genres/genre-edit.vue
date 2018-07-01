<template>
  <div>
    <el-row id="test">
      <el-col :span="12">
        <el-form :inline="true" ref="genre" :model="genre" label-width="120px">
          <el-form-item
            prop="name"
            :rules="[
              { required: true, message: 'Genre name is required'}
            ]"
            label="Genre type">
            <el-input v-model="genre.name"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit">Edit</el-button>
            <el-button @click="turnToPage">Back</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'genre-edit',
      created () {
        this.fetchSingleData()
      },
      methods: {
        onSubmit () {
          this.$http.put(`http://localhost:5000/api/genres/${this.genre.id}`, this.genre)
            .then((response) => {
              this.$message({
                type: 'success',
                message: 'Genre updated.'
              })
            }, response => { console.log(response.error) })
        },
        turnToPage () {
          this.$router.go(-1)
        },
        fetchSingleData () {
          this.$http.get(`http://localhost:5000/api/genres/${this.$route.params.id}`)
            .then((response) => { this.genre = response.body },
                    response => { console.log(response) })
        }
      },
      data () {
        return {
          genre: {
            id: null,
            name: null
          }
        }
      }
    }
</script>

<style scoped>
  #test {
    margin-top: 60px;
  }
</style>
