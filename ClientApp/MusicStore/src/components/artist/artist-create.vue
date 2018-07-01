<template>
  <el-row>
    <el-col>
      <el-form ref="artist" :model="artist" label-width="120px">
        <el-form-item label="Artist name">
          <el-input v-model="artist.name"></el-input>
        </el-form-item>
        <el-form-item label="Biography">
          <el-input v-model="artist.shortBio"></el-input>
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
      name: 'artist-create',
      methods: {
        onSubmit () {
          this.$http.post('http://localhost:5000/api/artists/', this.artist)
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
          artist: {
            name: null,
            shortBio: null
          }
        }
      }

    }
</script>

<style scoped>

</style>
