<template>
  <el-row>
    <el-col>
      <el-form ref="genre" :model="genre" label-width="120px">
        <el-form-item label="Genre name">
          <el-input v-model="genre.name"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit">Create</el-button>
          <el-button @click="turnToPage">Back</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>

<script>
    export default {
      name: 'genre-create',
      methods: {
        onSubmit () {
          this.$http.post('http://localhost:5000/api/genres/', this.genre)
            .then((response) => {
              this.$message({
                type: 'success',
                message: 'Genre created'
              })
              this.$router.go(-1)
            }, response => {
              this.$message({
                type: 'warning',
                message: 'Something went wrong'
              })
              console.log(response.error)
            })
        },
        turnToPage () {
          this.$router.go(-1)
        }
      },
      data () {
        return {
          genre: {}
        }
      }
    }
</script>

<style scoped>

</style>
